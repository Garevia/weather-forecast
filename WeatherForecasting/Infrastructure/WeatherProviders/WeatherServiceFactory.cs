using Microsoft.Extensions.Options;
using StackExchange.Redis;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Domain.Enums;
using WeatherForecasting.Infrastructure.Decorators;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;
using WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient;

namespace WeatherForecasting.Infrastructure.WeatherProviders;

public class WeatherServiceFactory : IWeatherServiceFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<RedisOptions> _redisOptions;
    private readonly IConnectionMultiplexer _redisDb;
    public WeatherServiceFactory(
        IServiceProvider serviceProvider,
        IOptions<RedisOptions> redisOptions,
        IConnectionMultiplexer redisDb)
    {
        _redisDb = redisDb;
        _redisOptions = redisOptions;
        _serviceProvider = serviceProvider;
    }

    public IWeatherServiceClient GetWeatherServiceClient(WeatherProviderType provider)
    {
        IWeatherServiceClient baseServiceClient = provider switch
        {
            WeatherProviderType.OpenWeather => _serviceProvider.GetRequiredService<OpenWeatherMapServiceClient>(),
            WeatherProviderType.Weatherstack => _serviceProvider.GetRequiredService<WeatherstackServiceClient>(),
            _ => throw new ArgumentOutOfRangeException()
        };

        var loggingDecoratedService = new WeatherServiceClientLoggingDecorator(
            baseServiceClient, 
            _serviceProvider.GetRequiredService<ILogger<WeatherServiceClientLoggingDecorator>>());

        var cacheDecoratedService = new WeatherServiceClientCachingDecorator(loggingDecoratedService, 
            _redisOptions,
            _redisDb,
            _serviceProvider.GetRequiredService<ILogger<WeatherServiceClientCachingDecorator>>(),
            provider);
        
        return cacheDecoratedService;
    }
}
