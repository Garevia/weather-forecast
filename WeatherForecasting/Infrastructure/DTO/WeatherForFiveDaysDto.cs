namespace WeatherForecasting.Infrastructure.DTO;

public class WeatherForFiveDaysDto
{
    public string City { get; set; }

    public string CountryCode { get; set; }
    
    public IEnumerable<WeatherDto> Forecasts { get; set; }
}