namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

/// <summary>
/// Model for retrieving data configuration for OpenMapWeather
/// </summary>
public class OpenMapWeatherApiOptions
{
    public string ApiKey { get; set; } = string.Empty;

    public string BaseUrl { get; set; }
}