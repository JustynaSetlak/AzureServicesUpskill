using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.Extensions.Options;
using Orders.Common.Config;
using Orders.Search.Interfaces;
using Orders.Search.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Search.Providers
{
    public class SearchService<T> : ISearchService<T> where T : class, ISearchable
    {
        private readonly ISearchIndexClient _serviceClient;

        public SearchService(IOptions<SearchServiceConfig> searchServiceConfig)
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

        public async Task<List<T>> Search(string searchText, SearchParameters searchParameters)
        {
            var searchResult = await _serviceClient.Documents.SearchAsync<T>(searchText, searchParameters);

            var resultList = searchResult.Results.Select(x => x.Document).ToList();

            return resultList;
        }
    }
}
