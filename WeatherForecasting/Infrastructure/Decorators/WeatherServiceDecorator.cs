using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Domain.Entities;

namespace WeatherForecasting.Infrastructure.Decorators;

public abstract class WeatherServiceDecorator : IWeatherService
{
    protected readonly IWeatherService _service;

    protected WeatherServiceDecorator(IWeatherService inner)
    {
        _service = inner;
    }

    public abstract Task<WeatherForecast> GetWeatherForecastByCityAsync(string city, string country);
    
    public abstract Task<WeatherForecast> GetWeatherForecastByLonAndLanAsync(double lon, double lat);
    
    public abstract Task<WeatherForecastForFiveDays> GetFiveDayForecastAsync(double lon, double lat);
}
