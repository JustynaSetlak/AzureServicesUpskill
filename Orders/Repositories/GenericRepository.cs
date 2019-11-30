using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Orders.Config;
using Orders.Repositories.Interfaces;
using System.Threading.Tasks;

namespace Orders.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ITableEntity
    {
        private readonly CloudTable _table;

        public GenericRepository(IOptions<ProductsStorageConfig> productsStorageConfig)
        {
            var storageAccount = CloudStorageAccount.Parse(productsStorageConfig.Value.ConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();

            _table = tableClient.GetTableReference(productsStorageConfig.Value.CategoryTable);
        }

        public async Task<T> Get(string partitionKey, string rowKey)
        {
            var retrieve = TableOperation.Retrieve<T>(partitionKey, rowKey);

            var retrieveResult = await _table.ExecuteAsync(retrieve);
            var element = (T)retrieveResult.Result;

            return element;
        }

        public async Task<TableResult> Insert(T element)
        {
            var insertOperation = TableOperation.Insert(element);
            var result = await _table.ExecuteAsync(insertOperation);

            return result;
        }

        public async Task<TableResult> Replace(T element)
        {
            var replace = TableOperation.Replace(element);
            var result = await _table.ExecuteAsync(replace);

            return result;
        }

        public async Task<TableResult> Delete(T element)
        {
            var deleteOperation = TableOperation.Delete(element);
            var result = await _table.ExecuteAsync(deleteOperation);

            return result;
        }
    }
}
