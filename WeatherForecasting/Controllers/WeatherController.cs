using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IMediator _mediator;

    public WeatherController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("by-city/{provider}/{city}/{countryCode}")]
    public async Task<IActionResult> GetForecastByCityAsync(WeatherProviderType provider, string city,
        string countryCode)
    {
        var query = new GetWeatherForecastByCityQuery(city, countryCode, provider);
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess)
        {
            var status = result.Error?.HttpStatusCode ?? HttpStatusCode.BadRequest;
            return StatusCode((int)status, new { error = result.Error?.Message });
        }
        
        return Ok(result);
    }

    [HttpGet("by-long-lat/{provider}/{lon}/{lat}")]
    public async Task<IActionResult> GetForecastByLonAndLatAsync(WeatherProviderType provider, double lon, double lat)
    {
        var query = new GetWeatherForecastByLonAndLatQuery(lon, lat, provider);
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess)
        {
            var status = result.Error?.HttpStatusCode ?? HttpStatusCode.BadRequest;
            return StatusCode((int)status, new { error = result.Error?.Message });
        }
        
        return Ok(result);
    }

    [HttpGet("for-five-days-by-long-lat/{provider}/{lon}/{lat}")]
    public async Task<IActionResult> GetForecastForFiveDaysByLonAndLatAsync(WeatherProviderType provider, double lon,
        double lat)
    {
        var query = new GetWeatherForecastForFiveDaysByLonAndLatQuery(lat, lon, provider);
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess)
        {
            var status = result.Error?.HttpStatusCode ?? HttpStatusCode.BadRequest;
            return StatusCode((int)status, new { error = result.Error?.Message });
        }
        
        return Ok(result);
    }

    [HttpGet("for-five-days-by-city/{provider}/{city}/{countryCode}")]
    public async Task<IActionResult> GetForecastForFiveDaysByCityAsync(WeatherProviderType provider, string city,
        string countryCode)
    {
        var query = new GetWeatherForecastForFiveDaysByCityQuery(city, countryCode, provider);
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess)
        {
            var status = result.Error?.HttpStatusCode ?? HttpStatusCode.BadRequest;
            return StatusCode((int)status, new { error = result.Error?.Message });
        }
        
        return Ok(result);
    }

    [HttpGet("get-geocoding/{provider}/{city}/{countryCode}")]
    public async Task<IActionResult> GetGeocodingAsync(WeatherProviderType provider, string city, string countryCode)
    {
        var query = new GetGeocodingQuery(city, countryCode, provider);
        var result = await _mediator.Send(query);
        
        if (!result.IsSuccess)
        {
            var status = result.Error?.HttpStatusCode ?? HttpStatusCode.BadRequest;
            return StatusCode((int)status, new { error = result.Error?.Message });
        }

        return Ok(result);
    }
}
