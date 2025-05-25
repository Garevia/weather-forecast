namespace WeatherForecasting.Domain.Entities;

/// <summary>
/// Represents a 5-day weather forecast for a specific city and country,
/// containing a collection of daily weather forecasts.
/// </summary>
public class WeatherForecastForFiveDays
{
    public string City { get; set; }

    public string CountryCode { get; set; }
    
    public IEnumerable<WeatherForecast> Forecasts { get; set; }
}