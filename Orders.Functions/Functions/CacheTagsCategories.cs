using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Orders.Functions.Config;
using Orders.Functions.Models;
using Orders.Functions.Options;
using Orders.Functions.Services.Interfaces;

namespace Orders.Functions.Functions
{
    public class CacheTagsCategories
    {
        private const string TAG_TABLE_NAME = "Tag";
        private const string TAG_CACHE_KEY = "tags";
        private const string CATEGORY_TABLE_NAME = "Category";
        private const string CATEGORY_CACHE_KEY = "categories";

        private readonly ICacheService _cacheService;
        private readonly ITableStorageService _tableStorageService;

        public CacheTagsCategories(ICacheService cacheService, ITableStorageService tableStorageService)
        {
            _cacheService = cacheService;
            _tableStorageService = tableStorageService;
        }

        [FunctionName(nameof(CacheTagsCategories))]
        public async Task Run(
            [TimerTrigger("0 */1 * * * *")]TimerInfo timer,
            [Table(TAG_TABLE_NAME, Connection = nameof(TableStorageConfig.TableStorageConnectionString))] CloudTable tagCloudTable,
            [Table(CATEGORY_TABLE_NAME, Connection = nameof(TableStorageConfig.TableStorageConnectionString))] CloudTable categoryCloudTable)
        {
            var tags = await _tableStorageService.QueryAllData<Tag>(tagCloudTable);
            _cacheService.SetValueToCache(TAG_CACHE_KEY, JsonConvert.SerializeObject(tags));

            var categories = await _tableStorageService.QueryAllData<Category>(categoryCloudTable);
            _cacheService.SetValueToCache(CATEGORY_CACHE_KEY, JsonConvert.SerializeObject(categories));
        }
    }
}
