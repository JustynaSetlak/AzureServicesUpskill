using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using Orders.Config;
using Orders.DataAccess.Interfaces;
using System.Threading.Tasks;

namespace Orders.DataAccess.Repositories
{
    public class DatabaseConfigurationRepository : IDatabaseConfigurationRepository
    {
        private readonly ProductsStorageConfig _productStorageConfigOptions;
        private readonly OrdersDatabaseConfig _ordersDatabaseConfigOptions;
        private readonly IDocumentClient _documentClient;

        public DatabaseConfigurationRepository(IDocumentClient documentClient, IOptions<ProductsStorageConfig> productStorageConfigOptions, IOptions<OrdersDatabaseConfig> ordersDatabaseConfigOptions)
        {
            _productStorageConfigOptions = productStorageConfigOptions.Value;
            _ordersDatabaseConfigOptions = ordersDatabaseConfigOptions.Value;
            _documentClient = documentClient;
        }
            
        public async Task CreateProductsTableDatabaseIfNotExist()
        {
            var storageAccount = CloudStorageAccount.Parse(_productStorageConfigOptions.ConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();

            var categoryTable = tableClient.GetTableReference(_productStorageConfigOptions.CategoryTable);
            await categoryTable.CreateIfNotExistsAsync();

            var tagTable = tableClient.GetTableReference(_productStorageConfigOptions.TagTable);
            await tagTable.CreateIfNotExistsAsync();
        }

        public async Task CreateOrdersDocumentDatabaseIfNotExist()
        {
            await _documentClient.CreateDatabaseIfNotExistsAsync(new Database { Id = _ordersDatabaseConfigOptions.DatabaseName }).ConfigureAwait(false);
            await _documentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_ordersDatabaseConfigOptions.DatabaseName), new DocumentCollection { Id = _ordersDatabaseConfigOptions.OrdersCollectionName });
        }
    }
}
