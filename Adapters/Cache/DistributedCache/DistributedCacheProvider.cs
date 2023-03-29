using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace poc_abstract_factory_cache.Adapters.Cache.DistributedCache;

public class DistributedCacheProvider : IDistributedCacheProvider
{
    private readonly IDistributedCache _cache;

    public DistributedCacheProvider(IDistributedCache cache)
        => _cache = cache;

    public async Task<T> OnGetAsync<T>(string key, Func<CancellationToken, Task<(T value, DateTimeOffset expireAt)>> callback, CancellationToken cancellationToken)
    {
        var jsonContent = await _cache.GetStringAsync(key, cancellationToken);

        if (jsonContent is null)
        {
            var (value, expireAt) = await callback(cancellationToken);

            var cacheOptions = new DistributedCacheEntryOptions { AbsoluteExpiration = expireAt };
            await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), cacheOptions, cancellationToken);
            return value;
        }

        return JsonSerializer.Deserialize<T>(jsonContent) ?? throw new Exception("Invalid cache content");
    }
}
