using Microsoft.Extensions.Options;
using StackExchange.Redis;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Domain.Enums;
using WeatherForecasting.Infrastructure.Decorators;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;
using WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient;

namespace WeatherForecasting.Infrastructure.WeatherProviders;

public class GeolocationServiceFactory : IGeolocationServiceFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<RedisOptions> _redisOptions;
    private readonly ConnectionMultiplexer _redisDb;
    
    public GeolocationServiceFactory(
        IServiceProvider serviceProvider,
        IOptions<RedisOptions> redisOptions,
        ConnectionMultiplexer redisDb)
    {
        _redisDb = redisDb;
        _redisOptions = redisOptions;
        _serviceProvider = serviceProvider;
    }
    
    public IGeocodingServiceClient CreateGeolocationServiceClient(WeatherProvider provider)
    {
        IGeocodingServiceClient baseServiceClient = provider switch
        {
            WeatherProvider.OpenWeather => _serviceProvider.GetRequiredService<OpenWeatherGeocodingServiceClient>(),
            WeatherProvider.Weatherstack => _serviceProvider.GetRequiredService<WeatherstackGeocodingServiceClient>(),
            _ => throw new ArgumentOutOfRangeException()
        };

        var loggingDecoratedService = new ServiceClientLoggingDecorator(
            baseServiceClient, 
            _serviceProvider.GetRequiredService<ILogger<ServiceClientLoggingDecorator>>());

        var cacheDecoratedService = new ServiceClientCachingDecorator(loggingDecoratedService, 
            _redisOptions,
            _redisDb,
            _serviceProvider.GetRequiredService<ILogger<ServiceClientCachingDecorator>>());
        
        return cacheDecoratedService;
    }
}