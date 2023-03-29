using Microsoft.Extensions.DependencyInjection;
using poc_abstract_factory_cache.Adapters.Cache.DistributedCache;
using poc_abstract_factory_cache.Adapters.Cache.MemoryCache;

namespace poc_abstract_factory_cache.Adapters.Cache;

public class CacheConcreteFactory : IAbstractCacheFactory
{
    private readonly IServiceProvider _serviceProvider;
    private ICacheProvider _cacheProvider;

    public CacheConcreteFactory(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public ICacheProvider UseDistributedCache()
        => _cacheProvider = _serviceProvider.GetRequiredService<IDistributedCacheProvider>();

    public ICacheProvider UseInMemoryCache()
        => _cacheProvider = _serviceProvider.GetRequiredService<IMemoryCacheProvider>();

    public Task<T> OnGetAsync<T>(string key, Func<CancellationToken, Task<(T value, DateTimeOffset expireAt)>> callback, CancellationToken cancellationToken)
    {
        if (_cacheProvider is null)
            throw new Exception("Cannot define the cache provider");

        return _cacheProvider.OnGetAsync(key, callback, cancellationToken);
    }
    
}
