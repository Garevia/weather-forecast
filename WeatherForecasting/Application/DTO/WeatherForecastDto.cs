using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.DTO;

/// <summary>
/// Dto for representing the weather
/// </summary>
public class WeatherForecastDto
{
    public string City { get; set; }
    
    public string CountryCode { get; set; }
    public string Description { get; set; }
    public decimal TemperatureCelsius { get; set; }
    public DateTimeOffset DateTime { get; set; }

    public WeatherProviderType WeatherProvider { get; set; }
}