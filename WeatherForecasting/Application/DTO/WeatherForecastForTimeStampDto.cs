namespace WeatherForecasting.Application.Weather.DTO;

public class WeatherForecastForTimeStampDto
{
    public string Description { get; set; }
    public decimal TemperatureCelsius { get; set; }
    
    public DateTimeOffset DateTime { get; set; }
}