using WeatherForecasting.Domain.Entities;

namespace WeatherForecasting.Infrastructure.WeatherProviders.Common;

public interface IWeatherServiceClient
{
    Task<WeatherForecast> GetWeatherForecastByCityAsync(string city, string country);
    
    Task<WeatherForecast> GetWeatherForecastByLonAndLanAsync(double lon, double lat);
    
    Task<WeatherForecastForFiveDays> GetFiveDayForecastAsync(double lon, double lat);
}