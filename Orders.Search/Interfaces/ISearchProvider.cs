using System.Threading.Tasks;

namespace Orders.Search.Interfaces
{
    public interface ISearchProvider<T> where T : class, ISearchable
    {
        Task MergeOrUploadItem(T item);

        Task<T> Get(string id);
    }
}
