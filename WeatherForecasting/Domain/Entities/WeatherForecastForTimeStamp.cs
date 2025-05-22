namespace WeatherForecasting.Domain.Entities;

public class WeatherForecastForTimeStamp
{
    public string Description { get; set; }
    public decimal TemperatureCelsius { get; set; }
    
    public DateTimeOffset DateTime { get; set; }
}