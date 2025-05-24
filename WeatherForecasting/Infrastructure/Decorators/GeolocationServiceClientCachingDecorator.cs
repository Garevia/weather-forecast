using Microsoft.Extensions.Options;
using StackExchange.Redis;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Infrastructure.Common.Helpers;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

namespace WeatherForecasting.Infrastructure.Decorators;

public class GeolocationServiceClientCachingDecorator : IGeocodingServiceClient
{
    private readonly TimeSpan _redisCacheDuration;
    private readonly IDatabase _redisDb;
    private readonly ILogger<GeolocationServiceClientCachingDecorator> _logger;
    private readonly IGeocodingServiceClient _geocodingServiceClient;
    
    public GeolocationServiceClientCachingDecorator(
        IGeocodingServiceClient geocodingServiceClient,
        IOptions<RedisOptions> redisOptions,
        ConnectionMultiplexer redisDb,
        ILogger<GeolocationServiceClientCachingDecorator> logger)
    {
        _geocodingServiceClient = geocodingServiceClient;
        _logger = logger;
        _redisDb = redisDb.GetDatabase();;
        _redisCacheDuration = redisOptions.Value.TimeSpan ?? 
                              throw new ArgumentNullException("OpenWeatherMap redis API  is not configured");
    }
    
    public async Task<Geolocation> ResolveCoordinatesAsync(string city, string countryCode)
    {
        var cacheKey = $"geolocation:{city}:{countryCode}";

        return await CacheHelper.GetOrSetAsync(
            _redisDb,
            cacheKey,
            () => _geocodingServiceClient.ResolveCoordinatesAsync(city, countryCode),
            _redisCacheDuration,
            _logger);
    }
}