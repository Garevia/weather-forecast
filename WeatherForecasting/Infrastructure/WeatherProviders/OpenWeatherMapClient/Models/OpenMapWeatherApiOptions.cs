namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

public class OpenMapWeatherApiOptions
{
    public string ApiKey { get; set; } = string.Empty;

    public string BaseUrl { get; set; }
}