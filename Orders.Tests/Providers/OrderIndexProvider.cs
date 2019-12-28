using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Options;
using Orders.Common.Config;
using Orders.Search.Interfaces;
using Orders.Search.Models.SearchModels;
using System.Threading.Tasks;

namespace Orders.Search.Providers
{
    public class OrderIndexProvider : IOrderIndexProvider
    {
        private readonly ISearchServiceClientProvider _searchServiceProvider;
        private readonly SearchServiceConfig _searchServiceConfig;

        public OrderIndexProvider(ISearchServiceClientProvider searchServiceProvider, IOptions<SearchServiceConfig> searchServiceConfig)
        {
            _searchServiceProvider = searchServiceProvider;
            _searchServiceConfig = searchServiceConfig.Value;
        }

        public async Task Create()
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
