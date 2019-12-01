using Microsoft.Azure.Cosmos.Table;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class, ITableEntity
    {
        Task<Result<T>> Delete(T element);

        Task<Result<T>> Get(string partitionKey, string rowKey);

        Task<Result<T>> Insert(T element);

        Task<Result<T>> Replace(T element);
    }
}
