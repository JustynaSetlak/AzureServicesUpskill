using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.BusinessLogic.Interfaces;
using Orders.BusinessLogic.Services;
using Orders.Common.Config;
using Orders.Configuration.Interfaces;
using Orders.DataAccess.Repositories;
using Orders.DataAccess.Repositories.Interfaces;
using Orders.DataAccess.Storage;
using Orders.DataAccess.Storage.Interfaces;
using Orders.DataAccess.TableRepositories;
using Orders.DataAccess.TableRepositories.Interfaces;
using Orders.EventHandler.Events;
using Orders.EventHandler.Handlers;
using Orders.EventHandler.Interfaces;
using Orders.EventHandler.Providers;
using Orders.EventHandler.Services;
using Orders.HostedServices;
using Orders.Repositories;
using Orders.Search.Interfaces;
using Orders.Search.Providers;
using Orders.Search.Services.Interfaces;
using Orders.Services;
using Orders.Services.Interfaces;
using System;

namespace Orders.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static AutofacServiceProvider ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            RegisterServices(builder);
            RegisterDataAccessRepositories(builder, configuration);
            RegisterEventServices(builder);
            RegisterSearchServices(builder);

            builder.Populate(services);

            return new AutofacServiceProvider(builder.Build());            
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(IService).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
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

        private static void RegisterSearchServices(ContainerBuilder builder)
        {
            builder.RegisterType<OrderSearchService>().As<IOrderSearchService>();
            builder.RegisterType<OrderIndexProvider>().As<IOrderIndexProvider>();
            builder.RegisterType<SearchServiceClientProvider>().As<ISearchServiceClientProvider>();
            builder.RegisterGeneric(typeof(SearchService<>)).As(typeof(ISearchService<>));
        }

        private static void RegisterDataAccessRepositories(ContainerBuilder builder, IConfiguration configuration)
        {
            var ordersDatabaseConfig = configuration
                .GetSection(nameof(OrdersDatabaseConfig))
                .Get<OrdersDatabaseConfig>();

            builder
                .Register(x => new DocumentClient(new Uri(ordersDatabaseConfig.Url), ordersDatabaseConfig.Key))
                .As<IDocumentClient>();

            builder.RegisterType<BlobFileStorage>().As<IBlobFileStorage>();
            builder.RegisterType<BlobStorageClientProvider>().As<IBlobStorageClientProvider>();

            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<DatabaseConfigurationRepository>().As<IDatabaseConfigurationRepository>();

            builder.RegisterType<CategoryTableRepository>().As<ICategoryTableRepository>();
            builder.RegisterType<TagTableRepository>().As<ITagTableRepository>();
            builder.RegisterGeneric(typeof(GenericTableRepository<>)).As(typeof(IGenericTableRepository<>));
        }
    }
}
