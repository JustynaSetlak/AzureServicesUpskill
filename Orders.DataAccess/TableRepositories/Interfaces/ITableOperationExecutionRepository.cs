using Microsoft.Azure.Cosmos.Table;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.DataAccess.TableRepositories.Interfaces
{
    public interface ITableOperationExecutionRepository<T>
    {
        Task<DataResult<T>> ExecuteOperation(TableOperation operation);
    }
}
