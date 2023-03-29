using Microsoft.Extensions.Caching.Memory;

namespace poc_abstract_factory_cache.Adapters.Cache.MemoryCache;

public class MemoryCacheProvider : IMemoryCacheProvider
{
    private readonly IMemoryCache _cache;

    public MemoryCacheProvider(IMemoryCache cache)
        => _cache = cache;

    public async Task<T> OnGetAsync<T>(string key, Func<CancellationToken, Task<(T value, DateTimeOffset expireAt)>> callback, CancellationToken cancellationToken)
    {
        if(!_cache.TryGetValue(key, out T? value) || value is null)
        {
            var callbackValues = await callback(cancellationToken);
            value = callbackValues.value;

            var cacheOptions = new MemoryCacheEntryOptions() { AbsoluteExpiration = callbackValues.expireAt };
            _cache.Set(key, value, cacheOptions);
        }

        return value;
    }
}
