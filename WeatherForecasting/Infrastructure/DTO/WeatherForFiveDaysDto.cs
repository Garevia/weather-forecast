namespace WeatherForecasting.Infrastructure.DTO;

/// <summary>
/// Data Transfer Object representing a 5-day weather forecast for a specific city and country,
/// containing a collection of daily weather data snapshots.
/// </summary>
public class WeatherForFiveDaysDto
{
    public string City { get; set; }

    public string CountryCode { get; set; }
    
    public IEnumerable<WeatherDto> Forecasts { get; set; }
}