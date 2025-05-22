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

    [HttpGet("by-city/{provider}/{city}")]
    public async Task<IActionResult> GetForecastByCityAsync(WeatherProvider provider, string city, string countryCode)
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
    public async Task<IActionResult> GetForecastByLonAndLatAsync(WeatherProvider provider, double lon, double lat)
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
    
    [HttpGet("for-five-days/{provider}/{lon}/{lat}")]
    public async Task<IActionResult> GetForecastForFiveDaysAsync(WeatherProvider provider, double lon, double lat)
    {
        var query = new GetWeatherForecastForFiveDaysQuery(lon, lat, provider);
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
