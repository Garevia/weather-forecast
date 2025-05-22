namespace WeatherForecasting.Application.Utilities;

public static class TimeHelper
{
    public static DateTimeOffset FromUnixTimeSeconds(long seconds)
        => DateTimeOffset.FromUnixTimeSeconds(seconds);

    public static long ToUnixTimeSeconds(DateTimeOffset dateTime)
        => dateTime.ToUnixTimeSeconds();
}