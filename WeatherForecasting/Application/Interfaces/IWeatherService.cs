using WeatherForecasting.Domain.Entities;

namespace WeatherForecasting.Application.Interfaces;

public interface IWeatherService
{
    Task<WeatherForecast> GetWeatherForecastByCityAsync(string city, string country);
    
    Task<WeatherForecast> GetWeatherForecastByLonAndLanAsync(double lon, double lat);
    
    Task<WeatherForecastForFiveDays> GetFiveDayForecastAsync(double lon, double lat);
}