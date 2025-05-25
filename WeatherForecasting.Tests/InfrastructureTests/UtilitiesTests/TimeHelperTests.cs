using WeatherForecasting.Infrastructure.Utilities;

namespace WeatherForecasting.Tests.InfrastructureTests.UtilitiesTests;

public class TimeHelperTests
{
    [Fact]
    public void FromUnixTimeSeconds_ShouldReturnCorrectDateTimeOffset()
    {
        // Arrange
        long unixTime = 1_600_000_000; // corresponds roughly to 2020-09-13T12:26:40Z
        var expected = DateTimeOffset.FromUnixTimeSeconds(unixTime);

        // Act
        var actual = TimeHelper.FromUnixTimeSeconds(unixTime);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToUnixTimeSeconds_ShouldReturnCorrectUnixTime()
    {
        // Arrange
        var dateTime = new DateTimeOffset(2020, 09, 13, 12, 26, 40, TimeSpan.Zero);
        long expected = 1_600_000_000;

        // Act
        var actual = TimeHelper.ToUnixTimeSeconds(dateTime);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void RoundTripConversion_ShouldReturnOriginalValue()
    {
        // Arrange
        long originalUnixTime = 1_600_000_000;

        // Act
        var dateTime = TimeHelper.FromUnixTimeSeconds(originalUnixTime);
        var unixTime = TimeHelper.ToUnixTimeSeconds(dateTime);

        // Assert
        Assert.Equal(originalUnixTime, unixTime);
    }
}