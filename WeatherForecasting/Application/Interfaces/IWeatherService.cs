using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Interfaces;

public interface IWeatherService
{
    Task<WeatherForecast> GetWeatherForecastByCityAsync(string city, string country, WeatherProvider provider);
    
    Task<WeatherForecast> GetWeatherForecastByLonAndLanAsync(double lon, double lat, WeatherProvider provider);
    
    Task<WeatherForecastForFiveDays> GetFiveDayForecastsAsync(double lon, double lat, WeatherProvider provider);
    
    Task<Geolocation> ResolveCoordinatesAsync(string city, string countryCode, WeatherProvider provider);
}