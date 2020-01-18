using Microsoft.Extensions.Options;
using Orders.Functions.Options;
using Orders.Functions.Services.Interfaces;
using StackExchange.Redis;

namespace Orders.Functions.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _cache;

        public CacheService(IOptions<CacheOptions> tableStorageOptions)
        {
            _cache = ConnectionMultiplexer.Connect(tableStorageOptions.Value.ConnectionString).GetDatabase();
        }

        public void SetValueToCache(string key, string value)
        {
            _cache.StringSet(key, value);
        }
    }
}
