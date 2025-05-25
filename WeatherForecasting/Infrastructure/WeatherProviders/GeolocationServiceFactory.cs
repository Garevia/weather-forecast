using Microsoft.Extensions.Options;
using StackExchange.Redis;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Domain.Enums;
using WeatherForecasting.Infrastructure.Decorators;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;
using WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient;

namespace WeatherForecasting.Infrastructure.WeatherProviders;

public class GeolocationServiceFactory : IGeolocationServiceFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<RedisOptions> _redisOptions;
    private readonly IConnectionMultiplexer _redisDb;
    
    public GeolocationServiceFactory(
        IServiceProvider serviceProvider,
        IOptions<RedisOptions> redisOptions,
        IConnectionMultiplexer redisDb)
    {
        _redisDb = redisDb;
        _redisOptions = redisOptions;
        _serviceProvider = serviceProvider;
    }
    
    public IGeocodingServiceClient GetGeolocationServiceClient(WeatherProviderType provider)
    {
        
        IGeocodingServiceClient baseServiceClient = provider switch
        {
            WeatherProviderType.OpenWeather => _serviceProvider.GetRequiredService<OpenWeatherGeocodingServiceClient>(),
            WeatherProviderType.Weatherstack => _serviceProvider.GetRequiredService<WeatherstackGeocodingServiceClient>(),
            _ => throw new ArgumentOutOfRangeException()
        };

        var loggingDecoratedService = new GeolocationServiceClientLoggingDecorator(
            baseServiceClient, 
            _serviceProvider.GetRequiredService<ILogger<GeolocationServiceClientLoggingDecorator>>());

        var cacheDecoratedService = new GeolocationServiceClientCachingDecorator(
            loggingDecoratedService, 
            _redisOptions,
            _redisDb,
            _serviceProvider.GetRequiredService<ILogger<GeolocationServiceClientCachingDecorator>>(),
            provider);
        
        return cacheDecoratedService;
    }
}