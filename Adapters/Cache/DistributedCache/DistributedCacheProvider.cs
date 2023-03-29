using Microsoft.Extensions.Caching.Distributed;

namespace poc_abstract_factory_cache.Adapters.Cache.DistributedCache;

public class DistributedCacheProvider : ICacheFactory
{
    private readonly IDistributedCache _cache;

    public DistributedCacheProvider(IDistributedCache cache)
        => _cache = cache;

    public async Task<T> OnGetAsync<T>(string key, Func<Task<(T value, DateTimeOffset expireAt)>> callback, CancellationToken cancellationToken)
    {
        var jsonContent = await _cache.GetAsync(key,cancellationToken).ConfigureAwait(false);

        throw new NotImplementedException();
    }
}
