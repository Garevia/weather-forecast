namespace WeatherForecasting.Infrastructure.Common;

public class WeatherApiException : Exception
{
    public WeatherApiException(string message, Exception inner)
        : base(message, inner) { }
}