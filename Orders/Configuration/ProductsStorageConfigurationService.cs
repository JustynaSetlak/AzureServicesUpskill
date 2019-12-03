using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using Orders.Config;
using Orders.Configuration.Interfaces;
using System.Threading.Tasks;

namespace Orders.Configuration
{
    public class ProductsStorageConfigurationService : IProductsStorageConfigurationService
    {
        private readonly ProductsStorageConfig _productStorageConfigOptions;
        private readonly OrdersDatabaseConfig _ordersDatabaseConfigOptions;
        private readonly IDocumentClient _documentClient;

        public ProductsStorageConfigurationService(IDocumentClient documentClient, IOptions<ProductsStorageConfig> productStorageConfigOptions, IOptions<OrdersDatabaseConfig> ordersDatabaseConfigOptions)
        {
            _productStorageConfigOptions = productStorageConfigOptions.Value;
            _ordersDatabaseConfigOptions = ordersDatabaseConfigOptions.Value;
            _documentClient = documentClient;
        }

        public async Task CreateDatabaseIfNotExist()
        {
            //await CreateProductsDatabaseIfNotxist();
            //await CreateOrdersDatabaseIfNotExist();
        }

        private async Task CreateProductsDatabaseIfNotxist()
        {
            var storageAccount = CloudStorageAccount.Parse(_productStorageConfigOptions.ConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();

            var categoryTable = tableClient.GetTableReference(_productStorageConfigOptions.CategoryTable);
            await categoryTable.CreateIfNotExistsAsync();

            var tagTable = tableClient.GetTableReference(_productStorageConfigOptions.TagTable);
            await tagTable.CreateIfNotExistsAsync();
        }

        private async Task CreateOrdersDatabaseIfNotExist()
        {
            await _documentClient.CreateDatabaseIfNotExistsAsync(new Database { Id = _ordersDatabaseConfigOptions.DatabaseName }).ConfigureAwait(false);
            await _documentClient.CreateDocumentCollectionIfNotExistsAsync(UriFactory.CreateDatabaseUri(_ordersDatabaseConfigOptions.DatabaseName), new DocumentCollection { Id = _ordersDatabaseConfigOptions.OrdersCollectionName });
        }
    }
}
