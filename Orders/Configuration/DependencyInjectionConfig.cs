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
        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ProductTableDbConfig>(configuration.GetSection(nameof(ProductTableDbConfig)));
            services.Configure<OrdersDatabaseConfig>(configuration.GetSection(nameof(OrdersDatabaseConfig)));
            services.Configure<StorageConfig>(configuration.GetSection(nameof(StorageConfig)));
            services.Configure<SearchServiceConfig>(configuration.GetSection(nameof(SearchServiceConfig)));
            services.Configure<EventGridConfig>(configuration.GetSection(nameof(EventGridConfig)));

            services.AddScoped<IDatabaseConfigurationService, DatabaseConfigurationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IImageUploadService, ImageUploadService>();
            services.AddScoped<IOrderSearchService, OrderSearchService>();
            services.AddScoped(typeof(ISearchService<>), typeof(SearchService<>));

            services.AddScoped(typeof(IBaseTableDbGenericRepository<>), typeof(BaseTableDbGenericRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IDatabaseConfigurationRepository, DatabaseConfigurationRepository>();
            services.AddScoped<IBlobFileStorage, BlobFileStorage>();
            services.AddScoped<IOrderIndexProvider, OrderIndexProvider>();
            services.AddScoped<ISearchServiceClientProvider, SearchServiceClientProvider>();

            services.AddScoped<IOrderEventsPublishService, OrderEventsPublishService>();
            services.AddScoped<IEventGridClientProvider, EventGridClientProvider>();
            services.AddScoped<IEventDispatchService, EventDispatchService>();
            services.AddScoped<IEventHandler<NewOrderCreated>, NewOrderCreatedEventHandler>();
            services.AddScoped<IEventHandler<ImageAssignedToOrder>, ImageAssignedToOrderEventHandler>();
            services.AddScoped<IEventHandler<ImageUnussignedFromOrder>, ImageUnussignedFromOrderEventHandler>();

            services.AddHostedService<IndexingHostedService>();

            var ordersDatabaseConfig = configuration
                .GetSection(nameof(OrdersDatabaseConfig))
                .Get<OrdersDatabaseConfig>();

            services.AddScoped<IDocumentClient>(x => new DocumentClient(new Uri(ordersDatabaseConfig.Url), ordersDatabaseConfig.Key));
        }
    }
}
