using Microsoft.Extensions.Options;
using StackExchange.Redis;
using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.Common.Helpers;
using WeatherForecasting.Infrastructure.DTO;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Infrastructure.Decorators;

public class WeatherServiceClientCachingDecorator : IWeatherServiceClient
{
    private readonly TimeSpan _redisCacheDuration;
    private readonly IDatabase _redisDb;
    private readonly ILogger<WeatherServiceClientCachingDecorator> _logger;
    private readonly IWeatherServiceClient _weatherServiceClient;
    
    public WeatherServiceClientCachingDecorator(
        IWeatherServiceClient weatherServiceClient,
        IOptions<RedisOptions> redisOptions,
        ConnectionMultiplexer redisDb,
        ILogger<WeatherServiceClientCachingDecorator> logger)
    {
        _weatherServiceClient = weatherServiceClient;
        _logger = logger;
        _redisDb = redisDb.GetDatabase();;
        _redisCacheDuration = redisOptions.Value.TimeSpan ?? 
                              throw new ArgumentNullException("OpenWeatherMap redis API  is not configured");
    }
    
    public  async Task<Result<WeatherDto>> GetWeatherForecastByCityAsync(string city, string country)
    {
        var cacheKey = $"weather:{city}:{country}";

        return await CacheHelper.GetOrSetAsync(
            _redisDb,
            cacheKey,
            () => _weatherServiceClient.GetWeatherForecastByCityAsync(city, country),
            _redisCacheDuration,
            _logger);
    }

    public  async Task<Result<WeatherDto>> GetWeatherForecastByLonAndLanAsync(double longitude, double latitude)
    {
        var cacheKey = $"weather:{longitude}:{latitude}";

        return await CacheHelper.GetOrSetAsync(
            _redisDb,
            cacheKey,
            () => _weatherServiceClient.GetWeatherForecastByLonAndLanAsync(longitude, latitude),
            _redisCacheDuration,
            _logger);
        
    }

    public  async Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByLonAndLatAsync(double longitude, double latitude)
    {
        var cacheKey = $"weather:{longitude}:{latitude}";

        return await CacheHelper.GetOrSetAsync(
            _redisDb,
            cacheKey,
            () => _weatherServiceClient.GetFiveDayForecastByLonAndLatAsync(longitude, latitude),
            _redisCacheDuration,
            _logger);
    }

    public async Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByCityAsync(string city, string countryCode)
    {
        var cacheKey = $"weather:{city}:{countryCode}";

        return await CacheHelper.GetOrSetAsync(
            _redisDb,
            cacheKey,
            () => _weatherServiceClient.GetFiveDayForecastByCityAsync(city, countryCode),
            _redisCacheDuration,
            _logger);
    }
}