using Microsoft.Extensions.Caching.Distributed;

namespace Mediscreen.API.Extensions;
public static class CacheOptions
{
    public static DistributedCacheEntryOptions DefaultExpiration => new()
    {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20)
    };
}
