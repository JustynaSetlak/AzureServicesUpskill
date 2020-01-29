using Microsoft.WindowsAzure.Storage.Table;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.TableStorage.Repositories.Interfaces
{
    public interface IGenericTableRepository<T> where T : class, ITableEntity
    {
        Task CreateTable();

        Task<DataResult<T>> Delete(T element);

        Task<DataResult<T>> Get(string partitionKey, string rowKey);

        Task<DataResult<T>> Insert(T element);

        Task<DataResult<T>> Replace(T element);

        Task<DataResult<T>> InsertOrMerge(T element);
    }
}
