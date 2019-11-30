using Microsoft.Azure.Cosmos.Table;
using System.Threading.Tasks;

namespace Orders.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : ITableEntity
    {
        Task<TableResult> Delete(T element);

        Task<T> Get(string partitionKey, string rowKey);

        Task<TableResult> Insert(T element);

        Task<TableResult> Replace(T element);
    }
}
