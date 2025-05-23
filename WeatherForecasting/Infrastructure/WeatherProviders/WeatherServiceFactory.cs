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

    public WeatherServiceFactory(IServiceProvider serviceProvider)
    {
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

        var decoratedService = new ServiceClientLoggingDecorator(
            baseServiceClient, 
            _serviceProvider.GetRequiredService<ILogger<ServiceClientLoggingDecorator>>());

        return decoratedService;
    }
    
    public IGeocodingServiceClient CreateGeolocationServiceClient(WeatherProvider provider)
    {
        IGeocodingServiceClient baseServiceClient = provider switch
        {
            WeatherProvider.OpenWeather => _serviceProvider.GetRequiredService<OpenWeatherGeocodingServiceClient>(),
            WeatherProvider.Weatherstack => _serviceProvider.GetRequiredService<WeatherstackGeocodingServiceClient>(),
            _ => throw new ArgumentOutOfRangeException()
        };

        var decoratedService = new ServiceClientLoggingDecorator(
            baseServiceClient, 
            _serviceProvider.GetRequiredService<ILogger<ServiceClientLoggingDecorator>>());

        return decoratedService;
    }
}
