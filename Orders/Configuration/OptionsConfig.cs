using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.EventHandler.Options;
using Orders.Search.Options;
using Orders.Storage.Options;
using Orders.TableStorage.Options;

namespace Orders.Api.Configuration
{
    public static class OptionsConfig
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TableStorageConfig>(configuration.GetSection(nameof(TableStorageConfig)));
            services.Configure<StorageConfig>(configuration.GetSection(nameof(StorageConfig)));
            services.Configure<SearchServiceConfig>(configuration.GetSection(nameof(SearchServiceConfig)));
            services.Configure<EventGridConfig>(configuration.GetSection(nameof(EventGridConfig)));
        }
    }
}
