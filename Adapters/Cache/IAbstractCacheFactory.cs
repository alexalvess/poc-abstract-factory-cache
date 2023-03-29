namespace poc_abstract_factory_cache.Adapters.Cache;

public interface IAbstractCacheFactory : ICacheProvider
{
    ICacheProvider UseInMemoryCache();
    
    ICacheProvider UseDistributedCache();
}
