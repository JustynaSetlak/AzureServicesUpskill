using Microsoft.Azure.Search;

namespace Orders.Search.Interfaces
{
    public interface ISearchServiceClientProvider
    {
        ISearchServiceClient Create(string searchServiceName, string key);
    }
}
