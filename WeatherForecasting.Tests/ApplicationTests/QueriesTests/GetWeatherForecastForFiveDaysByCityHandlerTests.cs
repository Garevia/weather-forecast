using Moq;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Tests.ApplicationTests.QueriesTests;


public class GetWeatherForecastForFiveDaysByCityHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsSuccessResult_WhenForecastIsAvailable()
    {
        // Arrange
        var mockWeatherService = new Mock<IWeatherService>();
        var query = new GetWeatherForecastForFiveDaysByCityQuery("Yerevan", "AM", WeatherProviderType.OpenWeather);
            
        var domainResult = new WeatherForecastForFiveDays
        {
            City = "Yerevan",
            CountryCode = "AM",
            Forecasts = new List<WeatherForecast>
            {
                new WeatherForecast("Yerevan", "AM", "Sunny", 1, DateTimeOffset.Now)
            }
        };

        mockWeatherService
            .Setup(ws => ws.GetFiveDayForecastsByCityAsync(query.City, query.CountryCode, query.Provider))
            .ReturnsAsync(Result<WeatherForecastForFiveDays>.Success(domainResult));

        var handler = new GetWeatherForecastForFiveDaysByCityHandler(mockWeatherService.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal("Yerevan", result.Value.City);
        Assert.Equal("AM", result.Value.CountryCode);
        Assert.Single(result.Value.Forecasts);
        Assert.Equal("Sunny", result.Value.Forecasts.First().Description);
    }

    [Fact]
    public async Task Handle_ReturnsFailureResult_WhenServiceFails()
    {
        // Arrange
        var mockWeatherService = new Mock<IWeatherService>();
        var query = new GetWeatherForecastForFiveDaysByCityQuery("UnknownCity", "AM", WeatherProviderType.OpenWeather);

        mockWeatherService
            .Setup(ws => ws.GetFiveDayForecastsByCityAsync(query.City, query.CountryCode, query.Provider))
            .ReturnsAsync(Result<WeatherForecastForFiveDays>.Failure("City not found"));

        var handler = new GetWeatherForecastForFiveDaysByCityHandler(mockWeatherService.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("City not found", result.Error.Message);
    }
}