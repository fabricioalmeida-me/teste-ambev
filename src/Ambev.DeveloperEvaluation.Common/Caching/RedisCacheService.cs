using System.Text.Json;
using Ambev.DeveloperEvaluation.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace Ambev.DeveloperEvaluation.Common.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var cachedData = await _cache.GetStringAsync(key, cancellationToken);
        return cachedData is null
            ? default
            : JsonSerializer.Deserialize<T>(cachedData);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan expiration, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        var jsonData = JsonSerializer.Serialize(value);
        await _cache.SetStringAsync(key, jsonData, options, cancellationToken);
    }
}