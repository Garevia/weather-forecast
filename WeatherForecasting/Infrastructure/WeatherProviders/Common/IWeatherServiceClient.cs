using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.DTO;

namespace WeatherForecasting.Infrastructure.WeatherProviders.Common;

/// <summary>
/// IWeatherServiceClient interface that implements 3rd party weather APIs
/// </summary>
public interface IWeatherServiceClient
{
    /// <summary>
    /// Retrieves the current weather forecast for a specified city and country.
    /// </summary>
    /// <param name="city">The city name.</param>
    /// <param name="country">The country code (e.g., "US").</param>
    /// <returns>A <see cref="Result{WeatherDto}"/> containing the weather data or error information.</returns>
    Task<Result<WeatherDto>> GetWeatherForecastByCityAsync(string city, string country);
    
    /// <summary>
    /// Retrieves the current weather forecast for specified geographic coordinates.
    /// </summary>
    /// <param name="longitude">Longitude coordinate.</param>
    /// <param name="latitude">Latitude coordinate.</param>
    /// <returns>A <see cref="Result{WeatherDto}"/> containing the weather data or error information.</returns>
    Task<Result<WeatherDto>> GetWeatherForecastByLonAndLanAsync(double longitude, double latitude);
    
    /// <summary>
    /// Retrieves a five-day weather forecast for specified geographic coordinates.
    /// </summary>
    /// <param name="longitude">Longitude coordinate.</param>
    /// <param name="latitude">Latitude coordinate.</param>
    /// <returns>A <see cref="Result{WeatherForFiveDaysDto}"/> containing the forecast or error information.</returns>
    Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByLonAndLatAsync(double longitude, double latitude);
    
    /// <summary>
    /// Retrieves a five-day weather forecast for a specified city and country.
    /// </summary>
    /// <param name="city">The city name.</param>
    /// <param name="countryCode">The country code (e.g., "US").</param>
    /// <returns>A <see cref="Result{WeatherForFiveDaysDto}"/> containing the forecast or error information.</returns>
    Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByCityAsync(string city, string countryCode);
}