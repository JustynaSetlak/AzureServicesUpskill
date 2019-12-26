using Microsoft.Azure.Search.Models;
using Orders.Search.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Search.Services.Interfaces
{
    public interface ISearchService<T> where T : class, ISearchable
    {
        Task MergeOrUploadItem(T item);

        Task<T> Get(string id);

        Task<List<T>> Search(string searchText, SearchParameters searchParameters);
    }
}
