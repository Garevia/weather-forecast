using System.Text.Json;
using StackExchange.Redis;

namespace WeatherForecasting.Infrastructure.Common.Helpers;

public static class CacheHelper
{
    public static async Task<T> GetOrSetAsync<T>(
        IDatabase cache,
        string key,
        Func<Task<T>> getData,
        TimeSpan expiration,
        ILogger logger)
    {
        var cached = await cache.StringGetAsync(key);
        if (cached.HasValue)
        {
            logger.LogInformation($"Cache hit: {key}");
            return JsonSerializer.Deserialize<T>(cached)!;
        }

        var data = await getData();
        await cache.StringSetAsync(
            key,
            JsonSerializer.Serialize(data),
            expiration);

        logger.LogInformation($"Cache set: {key}");
        return data;
    }
}