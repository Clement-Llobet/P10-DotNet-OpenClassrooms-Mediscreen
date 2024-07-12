using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Mediscreen.API.Extensions;

public static class CacheHelper
{
    public static async Task<T?> GetFromCacheAsync<T>(IDistributedCache cache, string cacheKey)
    {
        var cachedData = await cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<T>(cachedData);
        }
        return default;
    }

    public static async Task SetInCacheAsync<T>(IDistributedCache cache, string cacheKey, T data, DistributedCacheEntryOptions options)
    {
        var serializedData = JsonSerializer.Serialize(data);
        await cache.SetStringAsync(cacheKey, serializedData, options);
    }
}
