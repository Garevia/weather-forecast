namespace WeatherForecasting.Application.Weather.DTO;

public class WeatherForecastsDto
{
    public WeatherForecastsDto(List<WeatherForecastDto> weatherForecasts)
    {
        this.WeatherForecasts = weatherForecasts;
    }
    
    public List<WeatherForecastDto> WeatherForecasts { get; set; }
}