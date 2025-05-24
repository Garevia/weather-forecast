using FluentAssertions;
using Moq;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Enums;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.DTO;
namespace WeatherForecasting.Tests.ApplicationTests.QueriesTests;

public class GetGeocodingHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnGeolocationDto_WhenSuccess()
    {
        // Arrange
        var query = new GetGeocodingQuery("Yerevan", "AM", WeatherProviderType.OpenWeather);
        var location = new GeolocationDto(13d, 23d);

        var geocodingServiceClientMock = new Mock<IGeocodingServiceClient>();
        geocodingServiceClientMock
            .Setup(x => x.ResolveCoordinatesAsync(query.City, query.CountryCode))
            .ReturnsAsync(Result<GeolocationDto>.Success(location));

        var factoryMock = new Mock<IGeolocationServiceFactory>();
        factoryMock
            .Setup(f => f.GetGeolocationServiceClient(query.Provider))
            .Returns(geocodingServiceClientMock.Object);

        var handler = new GetGeocodingHandler(factoryMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Latitude.Should().Be(13d);
        result.Value.Longitude.Should().Be(23d);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnFailure_WhenGeocodingFails()
    {
        // Arrange
        var query = new GetGeocodingQuery("UnknownCity", "XX", WeatherProviderType.OpenWeather);

        var geocodingServiceClientMock = new Mock<IGeocodingServiceClient>();
        geocodingServiceClientMock
            .Setup(x => x.ResolveCoordinatesAsync(query.City, query.CountryCode))
            .ReturnsAsync(Result<GeolocationDto>.Failure("City not found"));

        var factoryMock = new Mock<IGeolocationServiceFactory>();
        factoryMock
            .Setup(f => f.GetGeolocationServiceClient(query.Provider))
            .Returns(geocodingServiceClientMock.Object);

        var handler = new GetGeocodingHandler(factoryMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().Be("City not found");
    }

}