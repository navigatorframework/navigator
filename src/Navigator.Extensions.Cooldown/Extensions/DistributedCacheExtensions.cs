using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Navigator.Extensions.Cooldown.Extensions;

internal static class DistributedCacheExtensions
{
    public static async Task<T?> GetAsync<T>(this IDistributedCache distributedCache, string key, CancellationToken cancellationToken = default)
    {
        var cachedItem = await distributedCache.GetAsync(key, cancellationToken);

        return cachedItem is not null ? JsonSerializer.Deserialize<T>(cachedItem) : default;
    }
}