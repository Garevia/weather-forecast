namespace WeatherForecasting.Application.Weather.DTO;

public class WeatherForecastForFiveDaysDto
{
    public string City { get; set; }

    public string CountryCode { get; set; }

    public List<WeatherForecastForTimeStampDto> WeatherForecasts { get; set; }
}