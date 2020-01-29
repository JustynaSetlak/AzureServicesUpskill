using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Orders.Results;
using Orders.TableStorage.Options;
using Orders.TableStorage.Providers.Interfaces;
using System.Net;
using System.Threading.Tasks;

namespace Orders.DataAccess.TableRepositories
{
    public class TableClientProvider<T> : ITableClientProvider<T> where T : class, ITableEntity
    {
        private readonly CloudTable _table;

        public TableClientProvider(IOptions<TableStorageConfig> productsStorageConfig)
        {
            var storageAccount = CloudStorageAccount.Parse(productsStorageConfig.Value.ConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();

            _table = tableClient.GetTableReference(typeof(T).Name);
        }

        public async Task CreateTable()
        {
            await _table.CreateIfNotExistsAsync();
        }

        public async Task<DataResult<T>> ExecuteOperation(TableOperation operation)
        {
            var executionResult = await _table.ExecuteAsync(operation);
            var result = GetResult(executionResult);
            return result;
        }

        private DataResult<T> GetResult(TableResult tableResult)
        {
            var isSuccessfull = tableResult.HttpStatusCode >= (int)HttpStatusCode.OK && tableResult.HttpStatusCode < (int)HttpStatusCode.Ambiguous;

            var result = new DataResult<T>(isSuccessfull, tableResult.Result as T);

            return result;
        }
    }
}
