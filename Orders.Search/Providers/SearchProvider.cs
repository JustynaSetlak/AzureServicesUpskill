using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Options;
using Orders.Config;
using Orders.Search.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Search.Providers
{
    public class SearchProvider<T> : ISearchProvider<T> where T : class, ISearchable
    {
        private readonly ISearchIndexClient _serviceClient;

        public SearchProvider(IOptions<SearchServiceConfig> searchServiceConfig)
        {
            var serviceClient = new SearchServiceClient(searchServiceConfig.Value.SearchServiceName, new SearchCredentials(searchServiceConfig.Value.ApiKey));
            _serviceClient = serviceClient.Indexes.GetClient(searchServiceConfig.Value.OrderIndexName);
        }

        public async Task<T> Get(string id)
        {
            var result = await _serviceClient.Documents.GetAsync<T>(id);

            return result;
        }

        public async Task MergeOrUploadItem(T item)
        {
            var itemsToUpload = new List<T> { item };

            var batch = IndexBatch.MergeOrUpload(itemsToUpload);

            try
            {
                await _serviceClient.Documents.IndexAsync(batch);
            }
            catch (IndexBatchException e)
            {
                //log e
            }
        }
    }
}
