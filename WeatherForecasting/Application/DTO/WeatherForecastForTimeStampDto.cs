namespace WeatherForecasting.Application.Weather.DTO;

/// <summary>
/// Dto describing the weather by hours
/// </summary>
public class WeatherForecastForTimeStampDto
{
    public string Description { get; set; }
    public decimal TemperatureCelsius { get; set; }
    public DateTimeOffset DateTime { get; set; }
}