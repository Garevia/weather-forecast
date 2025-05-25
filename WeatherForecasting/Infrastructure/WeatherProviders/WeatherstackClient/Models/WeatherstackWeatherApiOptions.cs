namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Models;

/// <summary>
/// Contains endpoint templates for interacting with the Weatherstack API.
/// </summary>
public class WeatherstackWeatherApiOptions
{
    public string ApiKey { get; set; } = string.Empty;

    public string BaseUrl { get; set; }
}