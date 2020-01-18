using Microsoft.WindowsAzure.Storage.Table;
using Orders.Functions.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Functions.Services
{
    public class TableStorageService : ITableStorageService
    {
        public async Task<List<T>> QueryAllData<T>(CloudTable cloudTable) where T : ITableEntity, new()
        {
            var tableQuery = new TableQuery<T>();
            var tagQueryResult = await cloudTable.ExecuteQuerySegmentedAsync(tableQuery, null);
            var data = tagQueryResult.Results;

            return data;
        }
    }
}
