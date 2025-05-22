using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Domain.Enums;
using WeatherForecasting.Infrastructure.Decorators;
using WeatherForecasting.Infrastructure.WeatherProviders;

namespace WeatherForecasting.Application.Services;

public class WeatherServiceFactory : IWeatherServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public WeatherServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IWeatherService Create(WeatherProvider provider)
    {
        IWeatherService baseService = provider switch
        {
            WeatherProvider.OpenWeather => _serviceProvider.GetRequiredService<OpenWeatherMapService>(),
            WeatherProvider.Weatherstack => _serviceProvider.GetRequiredService<WeatherstackService>(),
            _ => throw new ArgumentOutOfRangeException()
        };

        var decoratedService = new WeatherServiceLoggingDecorator(baseService, _serviceProvider.GetRequiredService<ILogger<WeatherServiceLoggingDecorator>>());

        return decoratedService;
    }
}
