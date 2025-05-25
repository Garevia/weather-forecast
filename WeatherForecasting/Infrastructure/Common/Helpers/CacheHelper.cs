using System.Text.Json;
using StackExchange.Redis;
using WeatherForecasting.Common;

namespace WeatherForecasting.Infrastructure.Common.Helpers;

public static class CacheHelper
{
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