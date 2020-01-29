using System.Threading.Tasks;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Options;
using Orders.Infrastructure.InitConfiguration.Interfaces;
using Orders.Search.Interfaces;
using Orders.Search.Models.SearchModels;
using Orders.Search.Options;

namespace Orders.Search.Handlers
{
    public class SearchConfigurationHandler : IConfigurationHandler
    {
        private readonly ISearchServiceClientProvider _searchServiceProvider;
        private readonly SearchServiceConfig _searchServiceConfig;

        public SearchConfigurationHandler(ISearchServiceClientProvider searchServiceProvider, IOptions<SearchServiceConfig> searchServiceConfig)
        {
            _searchServiceProvider = searchServiceProvider;
            _searchServiceConfig = searchServiceConfig.Value;
        }

        public async Task Configure()
        {
            var definition = new Index()
            {
                Name = _searchServiceConfig.OrderIndexName,
                Fields = FieldBuilder.BuildForType<OrderSearchModel>()
            };

            var searchServiceClient = _searchServiceProvider.Create(_searchServiceConfig.SearchServiceName, _searchServiceConfig.ApiKey);

            await searchServiceClient.Indexes.CreateOrUpdateAsync(definition);
        }
    }
}
