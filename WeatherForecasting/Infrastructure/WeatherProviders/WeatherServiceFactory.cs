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

public class WeatherServiceFactory : IWeatherServiceFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<RedisOptions> _redisOptions;
    private readonly ConnectionMultiplexer _redisDb;
    public WeatherServiceFactory(
        IServiceProvider serviceProvider,
        IOptions<RedisOptions> redisOptions,
        ConnectionMultiplexer redisDb)
    {
        _redisDb = redisDb;
        _redisOptions = redisOptions;
        _serviceProvider = serviceProvider;
    }

    public IWeatherServiceClient CreateWeatherServiceClient(WeatherProvider provider)
    {
        IWeatherServiceClient baseServiceClient = provider switch
        {
            WeatherProvider.OpenWeather => _serviceProvider.GetRequiredService<OpenWeatherMapServiceClient>(),
            WeatherProvider.Weatherstack => _serviceProvider.GetRequiredService<WeatherstackServiceClient>(),
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
