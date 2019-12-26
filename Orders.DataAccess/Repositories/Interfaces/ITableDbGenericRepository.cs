using Microsoft.Azure.Cosmos.Table;
using Orders.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.DataAccess.Repositories.Interfaces
{
    public interface IBaseTableDbGenericRepository<T> where T : class, ITableEntity
    {
        Task<DataResult<T>> Delete(T element);

        Task<DataResult<T>> Get(string partitionKey, string rowKey);

        Task<DataResult<T>> Insert(T element);

        Task<DataResult<T>> Replace(T element);
    }
}
