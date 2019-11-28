using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Options;
using Orders.Config;
using Orders.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orders.Configuration
{
    public class ProductsStorageConfigurationService : IProductsStorageConfigurationService
    {
        private readonly ProductsStorageConfig _productStorageConfigOptions;

        public ProductsStorageConfigurationService(IOptions<ProductsStorageConfig> productStorageConfigOptions)
        {
            _productStorageConfigOptions = productStorageConfigOptions.Value;
        }

        public async Task CreateDatabaseIfNotExist()
        {
            var storageAccount = CloudStorageAccount.Parse(_productStorageConfigOptions.ConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();

            var categoryTable = tableClient.GetTableReference(_productStorageConfigOptions.CategoryTable);
            await categoryTable.CreateIfNotExistsAsync();

            var tagTable = tableClient.GetTableReference(_productStorageConfigOptions.TagTable);
            await tagTable.CreateIfNotExistsAsync();
        }
    }
}
