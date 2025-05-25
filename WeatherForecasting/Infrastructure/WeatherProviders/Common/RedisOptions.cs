namespace WeatherForecasting.Infrastructure.WeatherProviders.Common;

/// <summary>
/// Model for retrieving data for redis
/// </summary>
public class RedisOptions
{
    public string Connection { get; set; }
    public TimeSpan? TimeSpan { get; set; }
}