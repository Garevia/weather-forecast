using WeatherForecasting.Common;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Interfaces;

public interface IWeatherService
{
    /// <summary>
    /// Method for getting weather for today by city and country 
    /// </summary>
    /// <param name="city">City by string</param>
    /// <param name="countryCode">Country code letters</param>
    /// <param name="provider">Provider Name</param>
    /// <returns>Weather info</returns>
    Task<Result<WeatherForecast>> GetWeatherForecastByCityAsync(string city, string countryCode, WeatherProviderType provider);
    
    /// <summary>
    /// Method for getting weather for today by latitude and longitude 
    /// </summary>
    /// <param name="longitude">Longitude</param>
    /// <param name="latitude">Latitiude</param>
    /// <param name="provider">Provider name</param>
    /// <returns>Weather info</returns>
    Task<Result<WeatherForecast>> GetWeatherForecastByLonAndLanAsync(double longitude, double latitude, WeatherProviderType provider);
    
    /// <summary>
    /// Method for getting weather for upcoming 5 days by latitude and longitude 
    /// </summary>
    /// <param name="longitude">Longitude</param>
    /// <param name="latitude">Latitiude</param>
    /// <param name="provider">Provider name</param>
    /// <returns>Weather info</returns>
    Task<Result<WeatherForecastForFiveDays>> GetFiveDayForecastsByLonAndLanAsync(double longitude, double latitude, WeatherProviderType provider);
    
    /// <summary>
    /// Method for getting weather for upcoming 5 days by city and country
    /// </summary>
    /// <param name="city">City by string</param>
    /// <param name="countryCode">Country code letters</param>
    /// <param name="provider">Provider Name</param>
    /// <returns>Weather info</returns>
    Task<Result<WeatherForecastForFiveDays>> GetFiveDayForecastsByCityAsync(string city, string countryCode, WeatherProviderType provider);
}