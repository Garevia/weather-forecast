namespace WeatherForecasting.Infrastructure.DTO;

public class WeatherDto
{
    public string City { get; set; } = default!;
    public string CountryCode { get; set; } = default!;
    public decimal TemperatureCelsius { get; set; }
    public DateTimeOffset Date { get; set; }
    public string Description { get; set; }
}