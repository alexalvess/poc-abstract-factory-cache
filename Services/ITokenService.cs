namespace poc_abstract_factory_cache.Services;

public interface ITokenService
{
    Task<string> UseWithInMemoryCache();

    Task<string> UseWithDistributedCache();
}
