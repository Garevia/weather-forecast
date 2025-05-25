using Moq;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Tests.ApplicationTests.QueriesTests;

public class GetWeatherForecastForFiveDaysByLonAndLatHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsSuccessResult_WhenForecastExists()
    {
        // Arrange
        var mockWeatherService = new Mock<IWeatherService>();
        var query = new GetWeatherForecastForFiveDaysByLonAndLatQuery(43d, 42d, WeatherProviderType.OpenWeather);

        var domainForecast = new WeatherForecastForFiveDays
        {
            City = "Yerevan",
            CountryCode = "AM",
            
                Forecasts = new List<WeatherForecast>
                {
                    new WeatherForecast("Yerevan", "AM", "Cloudy", 1, DateTimeOffset.Now)
                }
        };

        mockWeatherService
            .Setup(ws => ws.GetFiveDayForecastsByLonAndLanAsync(query.Longitude, query.Latitude, query.Provider))
            .ReturnsAsync(Result<WeatherForecastForFiveDays>.Success(domainForecast));

        var handler = new GetWeatherForecastForFiveDaysByLonAndLatHandler(mockWeatherService.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal("Yerevan", result.Value.City);
        Assert.Equal("AM", result.Value.CountryCode);
        Assert.Single(result.Value.Forecasts);
        Assert.Equal("Cloudy", result.Value.Forecasts.First().Description);
    }

    [Fact]
    public async Task Handle_ReturnsFailureResult_WhenServiceFails()
    {
        // Arrange
        var mockWeatherService = new Mock<IWeatherService>();
        var query = new GetWeatherForecastForFiveDaysByLonAndLatQuery(100d, 100d, WeatherProviderType.OpenWeather);


        mockWeatherService
            .Setup(ws => ws.GetFiveDayForecastsByLonAndLanAsync(query.Longitude, query.Latitude, query.Provider))
            .ReturnsAsync(Result<WeatherForecastForFiveDays>.Failure("Coordinates not found"));

        var handler = new GetWeatherForecastForFiveDaysByLonAndLatHandler(mockWeatherService.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Coordinates not found", result.Error.Message);
    }
}
