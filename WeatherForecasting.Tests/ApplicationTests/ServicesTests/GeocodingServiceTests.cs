using Moq;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Services;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Enums;
using WeatherForecasting.Infrastructure.DTO;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Tests.ApplicationTests.ServicesTests;

public class GeocodingServiceTests
{
    [Fact]
    public async Task ResolveCoordinatesAsync_ReturnsSuccess_WhenClientReturnsSuccess()
    {
        // Arrange
        var city = "Yerevan";
        var countryCode = "AM";
        var provider = WeatherProviderType.OpenWeather;

        var expectedDto = new GeolocationDto(40.1792d, 44.4991d);

        var clientMock = new Mock<IGeocodingServiceClient>();
        clientMock
            .Setup(c => c.ResolveCoordinatesAsync(city, countryCode))
            .ReturnsAsync(Result<GeolocationDto>.Success(expectedDto));

        var factoryMock = new Mock<IGeolocationServiceFactory>();
        factoryMock
            .Setup(f => f.GetGeolocationServiceClient(provider))
            .Returns(clientMock.Object);

        var service = new GeocodingService(factoryMock.Object);

        // Act
        var result = await service.ResolveCoordinatesAsync(city, countryCode, provider);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedDto.Latitude, result.Value.Latitude);
        Assert.Equal(expectedDto.Longitude, result.Value.Longitude);
    }

    [Fact]
    public async Task ResolveCoordinatesAsync_ReturnsFailure_WhenClientFails()
    {
        // Arrange
        var city = "Unknown";
        var countryCode = "XX";
        var provider = WeatherProviderType.Weatherstack;

        var clientMock = new Mock<IGeocodingServiceClient>();
        clientMock
            .Setup(c => c.ResolveCoordinatesAsync(city, countryCode))
            .ReturnsAsync(Result<GeolocationDto>.Failure("City not found"));

        var factoryMock = new Mock<IGeolocationServiceFactory>();
        factoryMock
            .Setup(f => f.GetGeolocationServiceClient(provider))
            .Returns(clientMock.Object);

        var service = new GeocodingService(factoryMock.Object);

        // Act
        var result = await service.ResolveCoordinatesAsync(city, countryCode, provider);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("City not found", result.Error);
    }
}