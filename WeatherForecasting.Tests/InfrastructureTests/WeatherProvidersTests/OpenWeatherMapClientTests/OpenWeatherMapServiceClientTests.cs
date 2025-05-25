using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using WeatherForecasting.Infrastructure.Utilities;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

namespace WeatherForecasting.Tests.InfrastructureTests.WeatherProvidersTests.OpenWeatherMapClientTests;

public class OpenWeatherMapServiceClientTests
{
    private const string ApiKey = "fake-api-key";

    private OpenWeatherMapServiceClient CreateClient(HttpResponseMessage responseMessage)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        handlerMock.Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           )
           .ReturnsAsync(responseMessage)
           .Verifiable();

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com")
        };

        var loggerMock = new Mock<ILogger<OpenWeatherMapServiceClient>>();

        var options = Options.Create(new OpenMapWeatherApiOptions { ApiKey = ApiKey });

        return new OpenWeatherMapServiceClient(httpClient, loggerMock.Object, options);
    }

    [Fact]
    public async Task GetWeatherForecastByCityAsync_ReturnsSuccess_WhenResponseIsValid()
    {
        // Arrange
        var city = "London";
        var country = "GB";

        var fakeResponse = new OpenWeatherResponse
        {
            name = city,
            System = new Infrastructure.WeatherProviders.OpenWeatherMapClient.Models.System() { Country = country },
            dt = 1672531200, // example unix timestamp
            main = new Main { Temperature = 15.5f },
            weather = new System.Collections.Generic.List<Weather>
            {
                new Weather { Description = "Cloudy" }
            }
        };

        var json = JsonSerializer.Serialize(fakeResponse);

        var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        };

        var client = CreateClient(httpResponse);

        // Act
        var result = await client.GetWeatherForecastByCityAsync(city, country);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(city, result.Value.City);
        Assert.Equal(country, result.Value.CountryCode);
        Assert.Equal("Cloudy", result.Value.Description);
        Assert.Equal((decimal)15.5f, result.Value.TemperatureCelsius);
        Assert.Equal(TimeHelper.FromUnixTimeSeconds(1672531200), result.Value.Date);
    }

    [Fact]
    public async Task GetWeatherForecastByCityAsync_ReturnsFailure_WhenResponseIsUnsuccessful()
    {
        // Arrange
        var city = "London";
        var country = "GB";

        var httpResponse = new HttpResponseMessage(HttpStatusCode.BadRequest);

        var client = CreateClient(httpResponse);

        // Act
        var result = await client.GetWeatherForecastByCityAsync(city, country);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Error in getting data", result.Error.Message);
    }

    [Fact]
    public async Task GetWeatherForecastByCityAsync_ReturnsFailure_OnException()
    {
        // Arrange
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        handlerMock.Protected()
           .Setup<Task<HttpResponseMessage>>(
              "SendAsync",
              ItExpr.IsAny<HttpRequestMessage>(),
              ItExpr.IsAny<CancellationToken>()
           )
           .ThrowsAsync(new Exception("Network failure"));

        var httpClient = new HttpClient(handlerMock.Object)
        {
            BaseAddress = new Uri("http://test.com")
        };

        var loggerMock = new Mock<ILogger<OpenWeatherMapServiceClient>>();
        var options = Options.Create(new OpenMapWeatherApiOptions { ApiKey = ApiKey });

        var client = new OpenWeatherMapServiceClient(httpClient, loggerMock.Object, options);

        // Act
        var result = await client.GetWeatherForecastByCityAsync("London", "GB");

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Unexpected error occurred", result.Error.Message);
    }
}