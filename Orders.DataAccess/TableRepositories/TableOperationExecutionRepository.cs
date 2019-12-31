using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Orders.Common.Config;
using Orders.DataAccess.TableRepositories.Interfaces;
using Orders.Results;
using System.Net;
using System.Threading.Tasks;

namespace Orders.DataAccess.TableRepositories
{
    public class TableOperationExecutionRepository<T> : ITableOperationExecutionRepository<T> where T : class, ITableEntity
    {
        private readonly CloudTable _table;

        public TableOperationExecutionRepository(IOptions<ProductTableDbConfig> productsStorageConfig)
        {
            var storageAccount = CloudStorageAccount.Parse(productsStorageConfig.Value.ConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();

            _table = tableClient.GetTableReference(typeof(T).Name);
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
