using FluentAssertions;
using Moq;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Tests.ApplicationTests.QueriesTests;

public class GetWeatherForecastByCityHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenForecastIsRetrieved()
    {
        // Arrange
        var query = new GetWeatherForecastByCityQuery("Yerevan", "AM", WeatherProviderType.OpenWeather);
        var domainForecast = new WeatherForecast(
            city: "Yerevan",
            countryCode: "AM",
            temperatureCelsius: 25.5m,
            dateTime: DateTime.UtcNow,
            description: "Sunny"
        );

        var weatherServiceMock = new Mock<IWeatherService>();
        weatherServiceMock
            .Setup(ws => ws.GetWeatherForecastByCityAsync(query.City, query.CountryCode, query.Provider))
            .ReturnsAsync(Result<WeatherForecast>.Success(domainForecast));

        var handler = new GetWeatherForecastByCityHandler(weatherServiceMock.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value.City.Should().Be("Yerevan");
        result.Value.CountryCode.Should().Be("AM");
        result.Value.TemperatureCelsius.Should().Be(25.5m);
        result.Value.Description.Should().Be("Sunny");
    }

    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenServiceReturnsError()
    {
        // Arrange
        var query = new GetWeatherForecastByCityQuery("UnknownCity", "XX", WeatherProviderType.OpenWeather);

        var weatherServiceMock = new Mock<IWeatherService>();
        weatherServiceMock
            .Setup(ws => ws.GetWeatherForecastByCityAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<WeatherProviderType>()))
            .ReturnsAsync(Result<WeatherForecast>.Failure("City not found"));
        
        var handler = new GetWeatherForecastByCityHandler(weatherServiceMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Message.Should().Be("City not found");
    }
}
