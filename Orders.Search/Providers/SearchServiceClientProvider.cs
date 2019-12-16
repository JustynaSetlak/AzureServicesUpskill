using Microsoft.Azure.Search;
using Orders.Search.Interfaces;

namespace Orders.Search.Providers
{
    public class SearchServiceClientProvider : ISearchServiceClientProvider
    {
        public ISearchServiceClient Create(string searchServiceName, string key)
        {
            var serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(key));
            return serviceClient;
        }
    }
}
