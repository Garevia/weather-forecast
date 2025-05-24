using Microsoft.Extensions.Options;
using StackExchange.Redis;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Infrastructure.Common.Helpers;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

namespace WeatherForecasting.Infrastructure.Decorators;

public class ServiceClientCachingDecorator : ServiceClientDecorator
{
    private readonly TimeSpan _redisCacheDuration;
    private readonly IDatabase _redisDb;
    private readonly ILogger<ServiceClientCachingDecorator> _logger;

    public ServiceClientCachingDecorator(
        IWeatherServiceClient weatherServiceClient,
        IOptions<RedisOptions> redisOptions,
        ConnectionMultiplexer redisDb,
         ILogger<ServiceClientCachingDecorator> logger) : base(weatherServiceClient)
    {
        _logger = logger;
        _redisDb = redisDb.GetDatabase();;
        _redisCacheDuration = redisOptions.Value.TimeSpan ?? 
                              throw new ArgumentNullException("OpenWeatherMap redis API  is not configured");
    }
    
    public override async Task<WeatherForecast> GetWeatherForecastByCityAsync(string city, string country)
    {
        var cacheKey = $"weather:{city}:{country}";

        return await CacheHelper.GetOrSetAsync(
            _redisDb,
            cacheKey,
            () => _weatherServiceClient.GetWeatherForecastByCityAsync(city, country),
            _redisCacheDuration,
            _logger);
    }

    public override async Task<WeatherForecast> GetWeatherForecastByLonAndLanAsync(double longitude, double latitude)
    {
        var cacheKey = $"weather:{longitude}:{latitude}";

        return await CacheHelper.GetOrSetAsync(
            _redisDb,
            cacheKey,
            () => _weatherServiceClient.GetWeatherForecastByLonAndLanAsync(longitude, latitude),
            _redisCacheDuration,
            _logger);
        
    }

    public override async Task<WeatherForecastForFiveDays> GetFiveDayForecastAsync(double longitude, double latitude)
    {
        var cacheKey = $"weather:{longitude}:{latitude}";

        return await CacheHelper.GetOrSetAsync(
            _redisDb,
            cacheKey,
            () => _weatherServiceClient.GetFiveDayForecastAsync(longitude, latitude),
            _redisCacheDuration,
            _logger);
    }

    public override async Task<WeatherForecastForFiveDays> GetFiveDayForecastAsync(string city, string countryCode)
    {
        var cacheKey = $"weather:{city}:{countryCode}";

        return await CacheHelper.GetOrSetAsync(
            _redisDb,
            cacheKey,
            () => _weatherServiceClient.GetFiveDayForecastAsync(city, countryCode),
            _redisCacheDuration,
            _logger);
        
    }

    public override async Task<Geolocation> ResolveCoordinatesAsync(string city, string countryCode)
    {
        var cacheKey = $"weather:{city}:{countryCode}";

        return await CacheHelper.GetOrSetAsync(
            _redisDb,
            cacheKey,
            () => _geocodingServiceClient.ResolveCoordinatesAsync(city, countryCode),
            _redisCacheDuration,
            _logger);
    }
}