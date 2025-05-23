namespace WeatherForecasting.Domain.Entities;

public class WeatherForecastForFiveDays
{
    public string City { get; set; }

    public string CountryCode { get; set; }
    
    public IEnumerable<WeatherForecast> Forecasts { get; set; }
}