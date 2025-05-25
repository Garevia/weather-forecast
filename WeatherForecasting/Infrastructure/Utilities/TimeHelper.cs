namespace WeatherForecasting.Infrastructure.Utilities;

/// <summary>
/// Provides helper methods for converting between Unix time (seconds since epoch)
/// and <see cref="DateTimeOffset"/> instances.
/// </summary>
public static class TimeHelper
{
    /// <summary>
    /// Converts a Unix time expressed in seconds since the Unix epoch to a <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="seconds">The number of seconds since January 1, 1970 (Unix epoch).</param>
    /// <returns>A <see cref="DateTimeOffset"/> representing the specified Unix time.</returns>
    public static DateTimeOffset FromUnixTimeSeconds(long seconds)
        => DateTimeOffset.FromUnixTimeSeconds(seconds);

    /// <summary>
    /// Converts a <see cref="DateTimeOffset"/> to Unix time expressed in seconds since the Unix epoch.
    /// </summary>
    /// <param name="dateTime">The date and time to convert.</param>
    /// <returns>The number of seconds since January 1, 1970 (Unix epoch) for the specified date and time.</returns>
    public static long ToUnixTimeSeconds(DateTimeOffset dateTime)
        => dateTime.ToUnixTimeSeconds();
}