﻿namespace poc_abstract_factory_cache.Adapters.Cache;

public interface ICacheProvider
{
    Task<T> OnGetAsync<T>(string key, Func<CancellationToken, Task<(T value, DateTimeOffset expireAt)>> callback, CancellationToken cancellationToken);
}
