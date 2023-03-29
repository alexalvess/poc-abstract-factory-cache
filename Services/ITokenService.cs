namespace poc_abstract_factory_cache.Services;

public interface ITokenService
{
    Task UseWithInMemoryCache();

    Task UseWithDistributedCache();
}
