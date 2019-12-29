using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Orders.Common.Config;
using Orders.DataAccess.TableRepositories.Interfaces;
using Orders.Results;
using System.Net;
using System.Threading.Tasks;

namespace Orders.DataAccess.TableRepositories
{
    public class GenericTableRepository<T> : IGenericTableRepository<T> where T : class, ITableEntity
    {
        private readonly CloudTable _table;

        public GenericTableRepository(IOptions<ProductTableDbConfig> productsStorageConfig)
        {
            var storageAccount = CloudStorageAccount.Parse(productsStorageConfig.Value.ConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();

            _table = tableClient.GetTableReference(typeof(T).Name);
        }

        public async Task<DataResult<T>> Get(string partitionKey, string id)
        {
            var retrieve = TableOperation.Retrieve<T>(partitionKey, id);

            var executionResult = await _table.ExecuteAsync(retrieve);
            var result = GetResult(executionResult);

            return result;
        }

        public async Task<DataResult<T>> Insert(T element)
        {
            var insertOperation = TableOperation.Insert(element);
            var executionResult = await _table.ExecuteAsync(insertOperation);

            var result = GetResult(executionResult);

            return result;
        }

        public async Task<DataResult<T>> Replace(T element)
        {
            var replace = TableOperation.Replace(element);
            var executionResult = await _table.ExecuteAsync(replace);

            var result = GetResult(executionResult);

            return result;
        }

        public async Task<DataResult<T>> Delete(T element)
        {
            var deleteOperation = TableOperation.Delete(element);
            var executionResult = await _table.ExecuteAsync(deleteOperation);

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
