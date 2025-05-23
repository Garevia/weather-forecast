using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Weather.DTO;

public class WeatherForecastForTimeStampDto
{
    public WeatherProvider Provider { get; set; }
    public string Description { get; set; }
    public decimal TemperatureCelsius { get; set; }
    public DateTimeOffset DateTime { get; set; }
}