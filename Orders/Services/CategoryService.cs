using Microsoft.Azure.Cosmos.Table;
using Orders.Models;
using Orders.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class CategoryService : ICategoryService
    {
        public async Task<bool> InsertCategory(Category newCategory)
        {
            var storageAccount = CloudStorageAccount.Parse("");
            var tableClient = storageAccount.CreateCloudTableClient();

            var categoryTable = tableClient.GetTableReference("categories");

            var insertOperation = TableOperation.Insert(newCategory);
            var result = await categoryTable.ExecuteAsync(insertOperation);
            return result.HttpStatusCode == (int)HttpStatusCode.OK;
        }
    }
}
