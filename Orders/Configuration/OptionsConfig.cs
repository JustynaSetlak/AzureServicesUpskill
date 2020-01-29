using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.EventHandler.Options;
using Orders.Search.Options;
using Orders.Storage.Options;

namespace Orders.Configuration
{
    public static class OptionsConfig
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            //services.Configure<OrdersDatabaseConfig>(configuration.GetSection(nameof(OrdersDatabaseConfig)));
            services.Configure<StorageConfig>(configuration.GetSection(nameof(StorageConfig)));
            services.Configure<SearchServiceConfig>(configuration.GetSection(nameof(SearchServiceConfig)));
            services.Configure<EventGridConfig>(configuration.GetSection(nameof(EventGridConfig)));
        }
    }
}
