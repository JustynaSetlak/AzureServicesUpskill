using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.BusinessLogic.DependencyModules;
using Orders.BusinessLogic.Services.Interfaces;
using Orders.DocumentDataAccess.DependencyModules;
using Orders.DocumentDataAccess.Options;
using Orders.EventHandler.DependencyModules;
using Orders.EventHandler.Handlers;
using Orders.EventHandler.Interfaces;
using Orders.EventHandler.Providers;
using Orders.EventHandler.Services;
using Orders.HostedServices;
using Orders.Infrastructure.DependencyModules;
using Orders.Search.DependencyModules;
using Orders.Search.Interfaces;
using Orders.Search.Providers;
using Orders.Search.Services.Interfaces;
using Orders.Storage.DependencyModules;
using Orders.TableStorage.DependencyModules;

namespace Orders.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static AutofacServiceProvider ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<StorageDependencyModule>();
            builder.RegisterModule<TableStorageDependencyModule>();
            builder.RegisterModule<SearchDependencyModule>();
            builder.RegisterModule<BusinessLogicDependencyModule>();
            builder.RegisterModule<InfrastructureDependencyModule>();
            builder.RegisterModule<EventDependencyModule>();

            var ordersDatabaseConfig = configuration.GetSection(nameof(OrdersDatabaseConfig)).Get<OrdersDatabaseConfig>();
            builder.RegisterModule(new DocumentDataAccessDependencyModule(ordersDatabaseConfig));

            builder.Populate(services);
            return new AutofacServiceProvider(builder.Build());
        }
    }
}
