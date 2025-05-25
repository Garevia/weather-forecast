namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient;


/// <summary>
/// Contains endpoint templates for interacting with the Weatherstack API.
/// </summary>
public class WeatherstackWeatherApiEndpoints
{
    public const string CurrentWeather = "current?access_key={0}&query={1},{2}";
}