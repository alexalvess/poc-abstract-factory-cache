using poc_abstract_factory_cache.Adapters.Cache;

namespace poc_abstract_factory_cache.Services;

public class TokenService : ITokenService
{
    private readonly IAbstractCacheFactory _cacheFactory;

	public TokenService(IAbstractCacheFactory cacheFactory)
        => _cacheFactory = cacheFactory;

    public async Task<string> UseWithInMemoryCache()
    {
        return await _cacheFactory
            .UseInMemoryCache()
            .OnGetAsync("key", (ct) =>
            {
                return Task.FromResult(("Memory cache content", DateTimeOffset.Now.AddSeconds(30)));
            }, default);
    }

    public async Task<string> UseWithDistributedCache()
    {
        return await _cacheFactory
            .UseDistributedCache()
            .OnGetAsync("key", (ct) =>
            {
                return Task.FromResult(("Distributed cache content", DateTimeOffset.Now.AddSeconds(30)));
            }, default);
    }
}
