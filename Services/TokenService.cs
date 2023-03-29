using poc_abstract_factory_cache.Adapters.Cache;

namespace poc_abstract_factory_cache.Services;

public class TokenService : ITokenService
{
    private readonly IAbstractCacheFactory _cacheFactory;

	public TokenService(IAbstractCacheFactory cacheFactory)
        => _cacheFactory = cacheFactory;

    public async Task UseWithInMemoryCache()
    {
        await _cacheFactory
            .UseInMemoryCache()
            .OnGetAsync("temp", (ct) =>
            {
                return Task.FromResult(("teste", DateTimeOffset.Now.AddSeconds(30)));
            }, default);
    }

    public async Task UseWithDistributedCache()
    {
        await _cacheFactory
            .UseDistributedCache()
            .OnGetAsync("temp", (ct) =>
            {
                return Task.FromResult(("teste", DateTimeOffset.Now.AddSeconds(30)));
            }, default);
    }
}
