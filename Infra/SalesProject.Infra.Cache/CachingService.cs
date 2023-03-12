using SalesProject.Domain.Interfaces.Service;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SalesProject.Domain.Entities.Base;

namespace SalesProject.Infra.Caching
{
    public class CachingService : ICachingService
    {
        private readonly IDistributedCache _cache;

        public CachingService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key) where T : Model
        {
            string? value = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(value))
                return default;

            return JsonConvert.DeserializeObject<T>(value);
        }

        public async Task<IEnumerable<T>> GetListAsync<T>(string key) where T : Model
        {
            string? value = await _cache.GetStringAsync(key);

            if (string.IsNullOrEmpty(value))
                return Enumerable.Empty<T>();

            var result = JsonConvert
                .DeserializeObject<List<T>>(value);

            return result ?? Enumerable.Empty<T>();
        }

        public async Task SetAsync<T>(string key, T model, int absoluteExpiration, int slidingExpiration) where T : Model
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(absoluteExpiration),
                SlidingExpiration = TimeSpan.FromSeconds(slidingExpiration)
            };

            string value = JsonConvert.SerializeObject
                (
                    model,
                    new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    }
                );

            await _cache.SetStringAsync(key, value, options);
        }

        public async Task DeleteAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task SetListAsync<T>(string key, IEnumerable<T> models, int absoluteExpiration, int slidingExpiration) where T : Model
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(absoluteExpiration),
                SlidingExpiration = TimeSpan.FromSeconds(slidingExpiration)
            };

            string value = JsonConvert.SerializeObject
                (
                    models,
                    new JsonSerializerSettings()
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    }
                );

            await _cache.SetStringAsync(key, value, options);
        }
    }
}