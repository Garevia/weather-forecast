using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

namespace WeatherForecasting.Tests.InfrastructureTests.WeatherProvidersTests.OpenWeatherMapClientTests;

public class OpenWeatherGeocodingServiceClientTests
{
    [Fact]
    public async Task ResolveCoordinatesAsync_ReturnsSuccess_WhenResponseIsSuccessful()
    {
        // Arrange
        var city = "London";
        var countryCode = "GB";
        var apiKey = "test-api-key";

        var expectedLat = 51.5074;
        var expectedLon = -0.1278;

        var mockOptions = Options.Create(new OpenMapWeatherApiOptions { ApiKey = apiKey });

        var responseContent = JsonSerializer.Serialize(new List<OpenWeatherGeolocationResponse>
        {
            new OpenWeatherGeolocationResponse { Lat = expectedLat, Lon = expectedLon }
        });

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        handlerMock.Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           )
           .ReturnsAsync(new HttpResponseMessage
           {
               StatusCode = HttpStatusCode.OK,
               Content = new StringContent(responseContent),
           })
           .Verifiable();

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com")
        };

        var client = new OpenWeatherGeocodingServiceClient(httpClient, mockOptions);

        // Act
        var result = await client.ResolveCoordinatesAsync(city, countryCode);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(expectedLat, result.Value.Latitude);
        Assert.Equal(expectedLon, result.Value.Longitude);

        handlerMock.Protected().Verify(
           "SendAsync",
           Times.Once(),
           ItExpr.Is<HttpRequestMessage>(req =>
               req.Method == HttpMethod.Get &&
               req.RequestUri!.ToString().Contains(city) &&
               req.RequestUri.ToString().Contains(countryCode) &&
               req.RequestUri.ToString().Contains(apiKey)
           ),
           ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task ResolveCoordinatesAsync_ReturnsFailure_WhenResponseIsUnsuccessful()
    {
        // Arrange
        var city = "London";
        var countryCode = "GB";
        var apiKey = "test-api-key";

        var mockOptions = Options.Create(new OpenMapWeatherApiOptions { ApiKey = apiKey });

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        handlerMock.Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           )
           .ReturnsAsync(new HttpResponseMessage
           {
               StatusCode = HttpStatusCode.BadRequest,
               Content = new StringContent("Bad request"),
           })
           .Verifiable();

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com")
        };

        var client = new OpenWeatherGeocodingServiceClient(httpClient, mockOptions);

        // Act
        var result = await client.ResolveCoordinatesAsync(city, countryCode);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Error in getting data", result.Error.Message);

        handlerMock.Protected().Verify(
           "SendAsync",
           Times.Once(),
           ItExpr.IsAny<HttpRequestMessage>(),
           ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task ResolveCoordinatesAsync_ReturnsFailure_WhenExceptionThrown()
    {
        // Arrange
        var city = "London";
        var countryCode = "GB";
        var apiKey = "test-api-key";

        var mockOptions = Options.Create(new OpenMapWeatherApiOptions { ApiKey = apiKey });

        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        handlerMock.Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           )
           .ThrowsAsync(new HttpRequestException("Network error"));

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com")
        };

        var client = new OpenWeatherGeocodingServiceClient(httpClient, mockOptions);

        // Act
        var result = await client.ResolveCoordinatesAsync(city, countryCode);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Unexpected error occurred", result.Error.Message);
    }
}