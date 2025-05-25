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

    /// <summary>
    /// Controller method for retrieving today's weather info 
    /// </summary>
    /// <param name="provider">Provider name</param>
    /// <param name="city">City </param>
    /// <param name="countryCode">Country code</param>
    /// <returns>Weather model</returns>
    [HttpGet("by-city")]
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
    
    /// <summary>
    /// Controller method for retrieving today's weather info 
    /// </summary>
    /// <param name="provider">Provider name</param>
    /// <param name="lon">Longitude </param>
    /// <param name="lat">Latitude</param>
    /// <returns>Weather model</returns>
    [HttpGet("by-long-lat")]
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

    /// <summary>
    /// Controller method for retrieving five day's broadcast weather info 
    /// </summary>
    /// <param name="provider">Provider name</param>
    /// <param name="lon">Longitude </param>
    /// <param name="lat">Latitude</param>
    /// <returns>Weather model</returns>
    [HttpGet("for-five-days-by-long-lat")]
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

    /// <summary>
    /// Controller method for retrieving five day's broadcast weather info 
    /// </summary>
    /// <param name="provider">Provider name</param>
    /// <param name="city">City </param>
    /// <param name="countryCode">Country code</param>
    /// <returns>Weather model</returns>
    [HttpGet("for-five-days-by-city")]
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

    /// <summary>
    /// Controller method for retrieving city longitude and latitude
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="city"></param>
    /// <param name="countryCode"></param>
    /// <returns></returns>
    [HttpGet("get-geocoding")]
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