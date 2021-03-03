using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Navigator
{
    internal static class DistributedCacheExtensions
    {
        public static async Task<T?> GetAsync<T>(this IDistributedCache distributedCache, string key, CancellationToken cancellationToken = default)
        {
            var cachedItem = await distributedCache.GetAsync(key, cancellationToken);

            return cachedItem is not null ? JsonSerializer.Deserialize<T>(cachedItem) : default;
        }
    }
}