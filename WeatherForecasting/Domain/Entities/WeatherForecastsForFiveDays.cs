namespace WeatherForecasting.Domain.Entities;

public class WeatherForecastsForFiveDays
{
    public WeatherForecastsForFiveDays(IEnumerable<WeatherForecastForFiveDays> forecasts)
    {
        this.Forecasts = forecasts;
    }
    
    public IEnumerable<WeatherForecastForFiveDays> Forecasts { get; set; }
}