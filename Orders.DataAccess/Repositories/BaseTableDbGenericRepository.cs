using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Orders.Config;
using Orders.Models;
using Orders.Repositories.Interfaces;
using Orders.Results;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Orders.Repositories
{
    public class BaseTableDbGenericRepository<T> : IBaseTableDbGenericRepository<T> where T : class, ITableEntity
    {
        private readonly CloudTable _table;

        public BaseTableDbGenericRepository(IOptions<ProductTableDbConfig> productsStorageConfig)
        {
            var storageAccount = CloudStorageAccount.Parse(productsStorageConfig.Value.ConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();

            _table = tableClient.GetTableReference(typeof(T).Name);
        }

        public async Task<Result<T>> Get(string partitionKey, string id)
        {
            var retrieve = TableOperation.Retrieve<T>(partitionKey, id);

            var executionResult = await _table.ExecuteAsync(retrieve);
            var result = GetResult(executionResult);

            return result;
        }

        public async Task<Result<T>> Insert(T element)
        {
            var insertOperation = TableOperation.Insert(element);
            var executionResult = await _table.ExecuteAsync(insertOperation);

            var result = this.GetResult(executionResult);
            
            return result;
        }

        public async Task<Result<T>> Replace(T element)
        {
            var replace = TableOperation.Replace(element);
            var executionResult = await _table.ExecuteAsync(replace);

            var result = this.GetResult(executionResult);

            return result;
        }

        public async Task<Result<T>> Delete(T element)
        {
            var deleteOperation = TableOperation.Delete(element);
            var executionResult = await _table.ExecuteAsync(deleteOperation);

            var result = this.GetResult(executionResult);
            return result;
        }

        private Result<T> GetResult(TableResult tableResult)
        {
            var isSuccessfull = ((int)tableResult.HttpStatusCode >= (int)HttpStatusCode.OK) && ((int)tableResult.HttpStatusCode < (int)HttpStatusCode.Ambiguous); ;

            var result = new Result<T>(isSuccessfull, tableResult.Result as T);

            return result;
        }
    }
}
