using WeatherForecasting.Common;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Interfaces;

public interface IWeatherService
{
    Task<Result<WeatherForecast>> GetWeatherForecastByCityAsync(string city, string countryCode, WeatherProviderType provider);
    
    Task<Result<WeatherForecast>> GetWeatherForecastByLonAndLanAsync(double longitude, double latitude, WeatherProviderType provider);
    
    Task<Result<WeatherForecastForFiveDays>> GetFiveDayForecastsByLonAndLanAsync(double longitude, double latitude, WeatherProviderType provider);
    
    Task<Result<WeatherForecastForFiveDays>> GetFiveDayForecastsByCityAsync(string city, string countryCode, WeatherProviderType provider);
}