namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Models;

public class WeatherstackWeatherApiOptions
{
    public string ApiKey { get; set; } = string.Empty;

    public string BaseUrl { get; set; }
}