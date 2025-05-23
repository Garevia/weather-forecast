namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

public class WeatherApiOptions
{
    public string ApiKey { get; set; } = string.Empty;

    public string BaseUrl { get; set; }
}