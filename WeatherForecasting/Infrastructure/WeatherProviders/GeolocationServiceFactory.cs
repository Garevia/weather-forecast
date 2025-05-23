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

    public GeolocationServiceFactory(IServiceProvider serviceProvider)
    {
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

        var decoratedService = new ServiceClientLoggingDecorator(
            baseServiceClient, 
            _serviceProvider.GetRequiredService<ILogger<ServiceClientLoggingDecorator>>());

        return decoratedService;
    }
}