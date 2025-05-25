using Moq;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Services;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Enums;
using WeatherForecasting.Infrastructure.DTO;
using WeatherForecasting.Infrastructure.WeatherProviders;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Tests.ApplicationTests.ServicesTests;

public class WeatherServiceTests
{
    private readonly Mock<IWeatherServiceFactory> _factoryMock;
    private readonly Mock<IWeatherServiceClient> _clientMock;
    private readonly WeatherService _weatherService;

    public WeatherServiceTests()
    {
        _factoryMock = new Mock<IWeatherServiceFactory>();
        _clientMock = new Mock<IWeatherServiceClient>();

        _weatherService = new WeatherService(_factoryMock.Object);
        _factoryMock.Setup(f => f.GetWeatherServiceClient(It.IsAny<WeatherProviderType>()))
            .Returns(_clientMock.Object);
    }
    
    [Fact]
    public async Task GetWeatherForecastByCityAsync_ReturnsSuccess()
    {
        var dto = new WeatherDto
        {
            City = "Yerevan",
            CountryCode = "AM",
            Description = "Sunny",
            TemperatureCelsius = 25.5m,
            Date = DateTime.Now
        };

        _clientMock
            .Setup(c => c.GetWeatherForecastByCityAsync("Yerevan", "AM"))
            .ReturnsAsync(Result<WeatherDto>.Success(dto));

        var result = await _weatherService.GetWeatherForecastByCityAsync("Yerevan", "AM", WeatherProviderType.Weatherstack);

        Assert.True(result.IsSuccess);
        Assert.Equal(dto.City, result.Value.City);
    }

    [Fact]
    public async Task GetWeatherForecastByCityAsync_ReturnsFailure()
    {
        _clientMock
            .Setup(c => c.GetWeatherForecastByCityAsync("Nowhere", "ZZ"))
            .ReturnsAsync(Result<WeatherDto>.Failure("City not found"));

        var result = await _weatherService.GetWeatherForecastByCityAsync("Nowhere", "ZZ", WeatherProviderType.Weatherstack);

        Assert.False(result.IsSuccess);
        Assert.Equal("City not found", result.Error.Message);
    }

    [Fact]
    public async Task GetWeatherForecastByLonAndLanAsync_ReturnsSuccess()
    {
        var dto = new WeatherDto
        {
            City = "Yerevan",
            CountryCode = "AM",
            Description = "Sunny",
            TemperatureCelsius = 28.0m,
            Date = DateTime.Now
        };

        _clientMock
            .Setup(c => c.GetWeatherForecastByLonAndLanAsync(44.5, 40.2))
            .ReturnsAsync(Result<WeatherDto>.Success(dto));

        var result = await _weatherService.GetWeatherForecastByLonAndLanAsync(44.5, 40.2, WeatherProviderType.Weatherstack);

        Assert.True(result.IsSuccess);
        Assert.Equal("Yerevan", result.Value.City);
    }

    [Fact]
    public async Task GetFiveDayForecastsByCityAsync_ReturnsSuccess()
    {
        var dto = new WeatherForFiveDaysDto
        {
            City = "Yerevan",
            CountryCode = "AM",
            Forecasts = new[]
            {
                new WeatherDto
                {
                    Date = DateTimeOffset.MaxValue,
                    Description = "Clear",
                    TemperatureCelsius = 22
                }
            }
        };

        _clientMock
            .Setup(c => c.GetFiveDayForecastByCityAsync("Yerevan", "AM"))
            .ReturnsAsync(Result<WeatherForFiveDaysDto>.Success(dto));

        var result = await _weatherService.GetFiveDayForecastsByCityAsync("Yerevan", "AM", WeatherProviderType.OpenWeather);

        Assert.True(result.IsSuccess);
        Assert.Equal("Yerevan", result.Value.City);
        Assert.Single(result.Value.Forecasts);
    }

    [Fact]
    public async Task GetFiveDayForecastsByLonAndLanAsync_ReturnsSuccess()
    {
        var dto = new WeatherForFiveDaysDto
        {
            City = "Yerevan",
            CountryCode = "AM",
            Forecasts = new[]
            {
                new WeatherDto
                {
                    Date = DateTimeOffset.Now,
                    Description = "Rain",
                    TemperatureCelsius = 18
                }
            }
        };

        _clientMock
            .Setup(c => c.GetFiveDayForecastByLonAndLatAsync(44.5, 40.2))
            .ReturnsAsync(Result<WeatherForFiveDaysDto>.Success(dto));

        var result = await _weatherService.GetFiveDayForecastsByLonAndLanAsync(44.5, 40.2, WeatherProviderType.Weatherstack);

        Assert.True(result.IsSuccess);
        Assert.Equal("Yerevan", result.Value.City);
        Assert.Single(result.Value.Forecasts);
    }

    [Fact]
    public async Task GetFiveDayForecastsByCityAsync_ReturnsFailure()
    {
        _clientMock
            .Setup(c => c.GetFiveDayForecastByCityAsync("Nowhere", "ZZ"))
            .ReturnsAsync(Result<WeatherForFiveDaysDto>.Failure("City not found"));

        var result = await _weatherService.GetFiveDayForecastsByCityAsync("Nowhere", "ZZ", WeatherProviderType.Weatherstack);

        Assert.False(result.IsSuccess);
        Assert.Equal("City not found", result.Error.Message);
    }
}
