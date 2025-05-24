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
    public async Task<IActionResult> GetForecastByCityAsync(WeatherProviderType provider, string city, string countryCode)
    {
        var query = new GetWeatherForecastByCityQuery(city, countryCode, provider);
        try
        {
            var service = await _mediator.Send(query);
            return Ok(service);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }
    
    [HttpGet("by-long-lat/{provider}/{lon}/{lat}")]
    public async Task<IActionResult> GetForecastByLonAndLatAsync(WeatherProviderType provider, double lon, double lat)
    {
        var query = new GetWeatherForecastByLonAndLatQuery(lon, lat, provider);
        try
        {
            var service = await _mediator.Send(query);
            return Ok(service);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }
    
    [HttpGet("for-five-days-by-long-lat/{provider}/{lon}/{lat}")]
    public async Task<IActionResult> GetForecastForFiveDaysByLonAndLatAsync(WeatherProviderType provider, double lon, double lat)
    {
        var query = new GetWeatherForecastForFiveDaysByLonAndLatQuery(lat, lon, provider);
        try
        {
            var service = await _mediator.Send(query);
            return Ok(service);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }
    
    [HttpGet("for-five-days-by-city/{provider}/{city}/{countryCode}")]
    public async Task<IActionResult> GetForecastForFiveDaysByCityAsync(WeatherProviderType provider, string city, string countryCode)
    {
        var query = new GetWeatherForecastForFiveDaysByCityQuery(city, countryCode, provider);
        try
        {
            var service = await _mediator.Send(query);
            return Ok(service);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }
    
    [HttpGet("get-geocoding/{provider}/{city}/{countryCode}")]
    public async Task<IActionResult> GetGeocodingAsync(WeatherProviderType provider, string city, string countryCode)
    {
        var query = new GetGeocodingQuery(city, countryCode, provider);
        try
        {
            var service = await _mediator.Send(query);
            return Ok(service);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal server error.");
        }
    }
}
