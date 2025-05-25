using Moq;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Tests.ApplicationTests.QueriesTests;

public class GetWeatherForecastByLonAndLatHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsSuccessResult_WhenWeatherServiceReturnsSuccess()
    {
        // Arrange
        var query = new GetWeatherForecastByLonAndLatQuery(44.5d, 40.2d, WeatherProviderType.OpenWeather);

        var domainForecast = new WeatherForecast("Yerevan", "AM", "Nice weather maybe", 43, DateTimeOffset.Now);
        var weatherServiceMock = new Mock<IWeatherService>();
        weatherServiceMock
            .Setup(ws => ws.GetWeatherForecastByLonAndLanAsync(query.Longitude, query.Latitude, query.Provider))
            .ReturnsAsync(Result<WeatherForecast>.Success(domainForecast));

        var handler = new GetWeatherForecastByLonAndLatHandler(weatherServiceMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(domainForecast.City, result.Value.City);
        Assert.Equal(domainForecast.CountryCode, result.Value.CountryCode);
        Assert.Equal(domainForecast.TemperatureCelsius, result.Value.TemperatureCelsius);
        Assert.Equal(query.Provider, result.Value.WeatherProvider);
        Assert.Equal(domainForecast.Description, result.Value.Description);
    }
    
    [Fact]
    public async Task Handle_ReturnsFailureResult_WhenWeatherServiceReturnsFailure()
    {
        // Arrange
        var query = new GetWeatherForecastByLonAndLatQuery(44.5d, 40.2d, WeatherProviderType.OpenWeather);

        var weatherServiceMock = new Mock<IWeatherService>();
        weatherServiceMock
            .Setup(ws => ws.GetWeatherForecastByLonAndLanAsync(query.Longitude, query.Latitude, query.Provider))
            .ReturnsAsync(Result<WeatherForecast>.Failure("Failed to get weather"));
        
        var handler = new GetWeatherForecastByLonAndLatHandler(weatherServiceMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Failed to get weather", result.Error.Message);
    }
}