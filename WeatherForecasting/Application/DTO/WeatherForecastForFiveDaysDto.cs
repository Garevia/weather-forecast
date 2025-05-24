using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.DTO;

public class WeatherForecastForFiveDaysDto
{ 
    public string City { get; set; }

    public string CountryCode { get; set; }
    
    public WeatherProviderType Provider { get; set; }
    public IEnumerable<WeatherForecastForTimeStampDto> Forecasts { get; set; }
}