using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Weather.DTO;

public class WeatherForecastDto
{
    public string City { get; set; }
    public string Description { get; set; }
    public decimal TemperatureCelsius { get; set; }
    public DateTimeOffset DateTime { get; set; }

    public WeatherProvider WeatherProvider { get; set; }
}