namespace WeatherForecasting.Domain.Entities;

public class WeatherForecastForFiveDays
{
    public string City { get; set; }

    public string CountryCode { get; set; }

    public List<WeatherForecastForTimeStamp> WeatherForecasts { get; set; }
}