using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Orders.BusinessLogic.Interfaces.Infrastructure;
using System.Text;
using System.Threading.Tasks;

namespace Orders.BusinessLogic.Services.Infrastructure
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T> GetValue<T>(string key) where T : class
        {
            var cacheValue = await _cache.GetStringAsync(key);

            if(cacheValue == null)
            {
                return null;
            }

            var result = JsonConvert.DeserializeObject<T>(cacheValue);

            return result;
        }
    }
}
