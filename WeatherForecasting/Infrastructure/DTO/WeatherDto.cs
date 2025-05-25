namespace WeatherForecasting.Infrastructure.DTO;

/// <summary>
/// Data Transfer Object representing a weather snapshot,
/// used for transferring weather data between layers or services.
/// </summary>
public class WeatherDto
{
    public string City { get; set; } = default!;
    public string CountryCode { get; set; } = default!;
    public decimal TemperatureCelsius { get; set; }
    public DateTimeOffset Date { get; set; }
    public string Description { get; set; }
}