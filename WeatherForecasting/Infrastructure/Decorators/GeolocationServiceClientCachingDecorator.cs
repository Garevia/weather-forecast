using Microsoft.Extensions.Options;
using StackExchange.Redis;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Enums;
using WeatherForecasting.Infrastructure.Common.Helpers;
using WeatherForecasting.Infrastructure.DTO;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Infrastructure.Decorators;

public class GeolocationServiceClientCachingDecorator : IGeocodingServiceClient
{
    private readonly TimeSpan _redisCacheDuration;
    private readonly IDatabase _redisDb;
    private readonly ILogger<GeolocationServiceClientCachingDecorator> _logger;
    private readonly IGeocodingServiceClient _geocodingServiceClient;
    private readonly WeatherProviderType _providerType;

    public GeolocationServiceClientCachingDecorator(
        IGeocodingServiceClient geocodingServiceClient,
        IOptions<RedisOptions> redisOptions,
        IConnectionMultiplexer redisDb,
        ILogger<GeolocationServiceClientCachingDecorator> logger,
        WeatherProviderType providerType)
    {
        _providerType = providerType;
        _geocodingServiceClient = geocodingServiceClient;
        _logger = logger;
        _redisDb = redisDb.GetDatabase();;
        _redisCacheDuration = redisOptions.Value.TimeSpan ?? 
                              throw new ArgumentNullException("OpenWeatherMap redis API  is not configured");
    }
    
    public async Task<Result<GeolocationDto>> ResolveCoordinatesAsync(string city, string countryCode)
    {
        var cacheKey = $"geolocation{_providerType}:{city}:{countryCode}";

        return await CacheHelper.GetOrSetAsync(
            _redisDb,
            cacheKey,
            () => _geocodingServiceClient.ResolveCoordinatesAsync(city, countryCode),
            _redisCacheDuration,
            _logger);
    }
}