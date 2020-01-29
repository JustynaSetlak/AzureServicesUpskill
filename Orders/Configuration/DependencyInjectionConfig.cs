using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.BusinessLogic.DependencyModules;
using Orders.BusinessLogic.Services.Interfaces;
using Orders.DocumentDataAccess.DependencyModules;
using Orders.DocumentDataAccess.Options;
using Orders.EventHandler.Handlers;
using Orders.EventHandler.Interfaces;
using Orders.EventHandler.Providers;
using Orders.EventHandler.Services;
using Orders.HostedServices;
using Orders.Search.DependencyModules;
using Orders.Search.Interfaces;
using Orders.Search.Providers;
using Orders.Search.Services.Interfaces;
using Orders.Storage.DependencyModules;
using Orders.TableStorage.DependencyModules;

namespace Orders.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static AutofacServiceProvider ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<StorageDependencyModule>();
            builder.RegisterModule<TableStorageDependencyModule>();
            builder.RegisterType<SearchDependencyModule>();
            builder.RegisterType<BusinessLogicDependencyModule>();

            var ordersDatabaseConfig = configuration.GetSection(nameof(OrdersDatabaseConfig)).Get<OrdersDatabaseConfig>();
            builder.RegisterModule(new DocumentDataAccessDependencyModule(ordersDatabaseConfig));

            RegisterEventServices(builder);

            builder.Populate(services);
            return new AutofacServiceProvider(builder.Build());            
        }

        private static void RegisterEventServices(ContainerBuilder builder)
        {
            builder.RegisterType<OrderEventsPublishService>().As<IOrderEventsPublishService>();
            builder.RegisterType<EventGridClientProvider>().As<IEventGridClientProvider>();
            builder.RegisterType<EventDispatchService>().As<IEventDispatchService>();

            builder
                .RegisterAssemblyTypes(typeof(IEventHandler<>).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
