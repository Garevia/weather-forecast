using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Common;
using WeatherForecasting.Controllers;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Tests.Controllers;

public class WeatherControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly WeatherController _controller;

    public WeatherControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new WeatherController(_mediatorMock.Object);
    }

    private Result<T> CreateErrorResult<T>(HttpStatusCode code, string message)
        => Result<T>.Failure(message, code);

    [Theory]
    [InlineData("Paris", "FR", WeatherProviderType.OpenWeather)]
    public async Task GetForecastByCityAsync_ReturnsOk_WhenSuccess(string city, string countryCode,
        WeatherProviderType provider)
    {
        var expectedResult = Result<WeatherForecastDto>.Success(new WeatherForecastDto());
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetWeatherForecastByCityQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var response = await _controller.GetForecastByCityAsync(provider, city, countryCode);

        var okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(expectedResult, okResult.Value);
    }

    [Fact]
    public async Task GetForecastByCityAsync_ReturnsError_WhenFailure()
    {
        var errorResult = CreateErrorResult<WeatherForecastDto>(HttpStatusCode.NotFound, "City not found");
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetWeatherForecastByCityQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(errorResult);

        var response = await _controller.GetForecastByCityAsync(WeatherProviderType.OpenWeather, "UnknownCity", "FR");

        var objResult = Assert.IsType<ObjectResult>(response);
        Assert.Equal((int)HttpStatusCode.NotFound, objResult.StatusCode);
    }

    [Fact]
    public async Task GetForecastByLonAndLatAsync_ReturnsOk_WhenSuccess()
    {
        var expectedResult = Result<WeatherForecastDto>.Success(new WeatherForecastDto());
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetWeatherForecastByLonAndLatQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var response = await _controller.GetForecastByLonAndLatAsync(WeatherProviderType.OpenWeather, 10, 20);

        var okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(expectedResult, okResult.Value);
    }

    [Fact]
    public async Task GetForecastByLonAndLatAsync_ReturnsError_WhenFailure()
    {
        var errorResult = CreateErrorResult<WeatherForecastDto>(HttpStatusCode.BadRequest, "Invalid coordinates");
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetWeatherForecastByLonAndLatQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(errorResult);

        var response = await _controller.GetForecastByLonAndLatAsync(WeatherProviderType.OpenWeather, 200, 200);

        var objResult = Assert.IsType<ObjectResult>(response);
        Assert.Equal((int)HttpStatusCode.BadRequest, objResult.StatusCode);
    }

    [Fact]
    public async Task GetForecastForFiveDaysByLonAndLatAsync_ReturnsOk_WhenSuccess()
    {
        var expectedResult = Result<WeatherForecastForFiveDaysDto>.Success(new WeatherForecastForFiveDaysDto());
        _mediatorMock.Setup(m =>
                m.Send(It.IsAny<GetWeatherForecastForFiveDaysByLonAndLatQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var response =
            await _controller.GetForecastForFiveDaysByLonAndLatAsync(WeatherProviderType.OpenWeather, 10, 20);

        var okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(expectedResult, okResult.Value);
    }

    [Fact]
    public async Task GetForecastForFiveDaysByLonAndLatAsync_ReturnsError_WhenFailure()
    {
        var errorResult =
            CreateErrorResult<WeatherForecastForFiveDaysDto>(HttpStatusCode.BadRequest, "Invalid coordinates");
        _mediatorMock.Setup(m =>
                m.Send(It.IsAny<GetWeatherForecastForFiveDaysByLonAndLatQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(errorResult);

        var response =
            await _controller.GetForecastForFiveDaysByLonAndLatAsync(WeatherProviderType.OpenWeather, 200, 200);

        var objResult = Assert.IsType<ObjectResult>(response);
        Assert.Equal((int)HttpStatusCode.BadRequest, objResult.StatusCode);
    }

    [Fact]
    public async Task GetForecastForFiveDaysByCityAsync_ReturnsOk_WhenSuccess()
    {
        var expectedResult = Result<WeatherForecastForFiveDaysDto>.Success(new WeatherForecastForFiveDaysDto());
        _mediatorMock.Setup(m =>
                m.Send(It.IsAny<GetWeatherForecastForFiveDaysByCityQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var response =
            await _controller.GetForecastForFiveDaysByCityAsync(WeatherProviderType.OpenWeather, "Paris", "FR");

        var okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(expectedResult, okResult.Value);
    }

    [Fact]
    public async Task GetForecastForFiveDaysByCityAsync_ReturnsError_WhenFailure()
    {
        var errorResult = CreateErrorResult<WeatherForecastForFiveDaysDto>(HttpStatusCode.NotFound, "City not found");
        _mediatorMock.Setup(m =>
                m.Send(It.IsAny<GetWeatherForecastForFiveDaysByCityQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(errorResult);

        var response =
            await _controller.GetForecastForFiveDaysByCityAsync(WeatherProviderType.OpenWeather, "UnknownCity", "FR");

        var objResult = Assert.IsType<ObjectResult>(response);
        Assert.Equal((int)HttpStatusCode.NotFound, objResult.StatusCode);
    }

    [Fact]
    public async Task GetGeocodingAsync_ReturnsOk_WhenSuccess()
    {
        var expectedResult = Result<GeolocationDto>.Success(new GeolocationDto());
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGeocodingQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        var response = await _controller.GetGeocodingAsync(WeatherProviderType.OpenWeather, "Paris", "FR");

        var okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(expectedResult, okResult.Value);
    }

    [Fact]
    public async Task GetGeocodingAsync_ReturnsError_WhenFailure()
    {
        var errorResult = CreateErrorResult<GeolocationDto>(HttpStatusCode.NotFound, "Location not found");
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetGeocodingQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(errorResult);

        var response = await _controller.GetGeocodingAsync(WeatherProviderType.OpenWeather, "UnknownCity", "FR");

        var objResult = Assert.IsType<ObjectResult>(response);
        Assert.Equal((int)HttpStatusCode.NotFound, objResult.StatusCode);
    }
}