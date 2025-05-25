using System.Text.Json;
using StackExchange.Redis;
using WeatherForecasting.Common;

namespace WeatherForecasting.Infrastructure.Common.Helpers;

/// <summary>
/// Class for caching
/// </summary>
public static class CacheHelper
{
    /// <summary>
    /// This method caches data if it is not cached, if it is cached it retreives the cache
    /// </summary>
    /// <param name="cache">Cache provider</param>
    /// <param name="key">Cache key</param>
    /// <param name="getData">Function that should be called or not called depending on cache result</param>
    /// <param name="expiration">Expiration time of the cache</param>
    /// <param name="logger">logger for logging</param>
    /// <typeparam name="T">Type of the class that can be cached</typeparam>
    /// <returns></returns>
    public static async Task<Result<T>> GetOrSetAsync<T>(
        IDatabase cache,
        string key,
        Func<Task<Result<T>>> getData,
        TimeSpan expiration,
        ILogger logger)
    {
        var cached = await cache.StringGetAsync(key);
        if (cached.HasValue)
        {
            logger.LogInformation($"Cache hit: {key}");
            return JsonSerializer.Deserialize<Result<T>>(cached)!;
        }

        var data = await getData();
        
        if (data.IsSuccess)
        {
            try
            {
                await cache.StringSetAsync(
                    key,
                    JsonSerializer.Serialize(data),
                    expiration);

                logger.LogInformation($"Cache set: {key}");
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, $"Cache set failed for key: {key}");
            }
        }
        else
        {
            logger.LogInformation($"Skipping cache set for failure result: {key}");
        }
        
        return data;
    }
}