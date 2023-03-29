using Microsoft.Extensions.DependencyInjection;
using poc_abstract_factory_cache.Adapters.Cache;
using poc_abstract_factory_cache.Adapters.Cache.DistributedCache;
using poc_abstract_factory_cache.Adapters.Cache.MemoryCache;
using poc_abstract_factory_cache.Services;

var serviceCollections = new ServiceCollection()
    .AddStackExchangeRedisCache(options => options.Configuration = "localhost:6379")
    .AddMemoryCache()
    .AddTransient<IDistributedCacheProvider, DistributedCacheProvider>()
    .AddTransient<IMemoryCacheProvider, MemoryCacheProvider>()
    .AddScoped<IAbstractCacheFactory, CacheConcreteFactory>()
    .AddScoped<ITokenService, TokenService>();

var builder = serviceCollections.BuildServiceProvider();

var service = builder.GetRequiredService<ITokenService>();

await service.UseWithInMemoryCache();
await service.UseWithDistributedCache();