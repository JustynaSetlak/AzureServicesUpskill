using Microsoft.WindowsAzure.Storage.Table;
using Orders.Results;
using System.Threading.Tasks;

namespace Orders.TableStorage.Providers.Interfaces
{
    public interface ITableClientProvider<T>
    {
        Task CreateTable();

        Task<DataResult<T>> ExecuteOperation(TableOperation operation);
    }
}
