using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Domain.Entities;

namespace WeatherForecasting.Infrastructure.Decorators;

public class WeatherServiceLoggingDecorator : WeatherServiceDecorator
{
    private readonly ILogger<WeatherServiceLoggingDecorator> _logger;

    public WeatherServiceLoggingDecorator(IWeatherService service, ILogger<WeatherServiceLoggingDecorator> logger)
        : base(service)
    {
        _logger = logger;
    }

    public override async Task<WeatherForecast> GetWeatherForecastByCityAsync(string city, string country)
    {
        _logger.LogInformation("Requesting weather for {City} at {Country}", city, country);

        try
        {
            var result = await _service.GetWeatherForecastByCityAsync(city, country);

            _logger.LogInformation("Weather received: {Temp}°C, {Description}", result.TemperatureCelsius, result.Description);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting weather for {City}", city);
            throw;
        }
    }

    public override async Task<WeatherForecast> GetWeatherForecastByLonAndLanAsync(double lon, double lat)
    {
        _logger.LogInformation("Requesting weather for {long} and {lat}", lon, lat);

        try
        {
            var result = await _service.GetWeatherForecastByLonAndLanAsync(lon, lat);

            _logger.LogInformation("Weather received: {Temp}°C, {Description}", result.TemperatureCelsius, result.Description);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting weather for {long} and {lat}", lon, lat);
            throw;
        }
    }

    public override async Task<WeatherForecastForFiveDays> GetFiveDayForecastAsync(double lon, double lat)
    {
        _logger.LogInformation("Requesting weather for {long} and {lat}", lon, lat);

        try
        {
            var result = await _service.GetFiveDayForecastAsync(lon, lat);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting weather for {long} and {lat}", lon, lat);
            throw;
        }    }
}