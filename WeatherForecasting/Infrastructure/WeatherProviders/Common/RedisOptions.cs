namespace WeatherForecasting.Infrastructure.WeatherProviders.Common;

public class RedisOptions
{
    public string Connection { get; set; }
    public TimeSpan? TimeSpan { get; set; }
}