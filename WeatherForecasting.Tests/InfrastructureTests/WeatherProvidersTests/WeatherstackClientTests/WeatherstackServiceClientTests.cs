using System.Globalization;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient;
using WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Models;

namespace WeatherForecasting.Tests.InfrastructureTests.WeatherProvidersTests.WeatherstackClientTests;

public class WeatherstackServiceClientTests
{
    private const string ApiKey = "fake-api-key";

    private WeatherstackServiceClient CreateClient(HttpResponseMessage responseMessage)
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

        var loggerMock = new Mock<ILogger<WeatherstackServiceClient>>();

        var options = Options.Create(new WeatherstackWeatherApiOptions { ApiKey = ApiKey });

        return new WeatherstackServiceClient(httpClient, loggerMock.Object, options);
    }

    [Fact]
    public async Task GetWeatherForecastByCityAsync_ReturnsSuccess_WhenResponseIsValid()
    {
        var city = "Paris";
        var country = "FR";

        var responseObj = new WeatherstackCurrentResponse
        {
            Location = new WeatherstackLocation
            {
                Name = city,
                Country = country,
                LocalTime = "2025-05-26 14:30"
            },
            Current = new WeatherstackCurrent
            {
                Temperature = 20,
                WeatherDescriptions = new System.Collections.Generic.List<string> { "Sunny", "Clear" }
            }
        };

        var json = JsonSerializer.Serialize(responseObj);

        var httpResponse = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(json)
        };

        var client = CreateClient(httpResponse);

        var result = await client.GetWeatherForecastByCityAsync(city, country);

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(city, result.Value.City);
        Assert.Equal(country, result.Value.CountryCode);
        Assert.Equal("Sunny,Clear", result.Value.Description);
        Assert.Equal(20, result.Value.TemperatureCelsius);
        Assert.Equal(DateTime.ParseExact("2025-05-26 14:30", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), result.Value.Date);
    }

    [Fact]
    public async Task GetWeatherForecastByCityAsync_ReturnsFailure_WhenResponseIsError()
    {
        var city = "Nowhere";
        var country = "ZZ";

        var error = new ClientError
        {
            Success = false,
            Error = new Error
            {
                Code = ErrorCode.NotFound,
                Type = "not_found",
                Info = "City not found"
            }
        };

        var json = JsonSerializer.Serialize(error);

        var httpResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
        {
            Content = new StringContent(json)
        };

        var client = CreateClient(httpResponse);

        var result = await client.GetWeatherForecastByCityAsync(city, country);

        Assert.False(result.IsSuccess);
        Assert.Contains("Error in getting data", result.Error.Message);
        Assert.Equal(HttpStatusCode.NotFound,  result.Error.HttpStatusCode);
    }

    [Fact]
    public async Task GetWeatherForecastByCityAsync_ReturnsFailure_OnException()
    {
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

        var loggerMock = new Mock<ILogger<WeatherstackServiceClient>>();
        var options = Options.Create(new WeatherstackWeatherApiOptions { ApiKey = ApiKey });

        var client = new WeatherstackServiceClient(httpClient, loggerMock.Object, options);

        var result = await client.GetWeatherForecastByCityAsync("Paris", "FR");

        Assert.False(result.IsSuccess);
        Assert.Contains("Unexpected error occurred", result.Error.Message);
    }
}