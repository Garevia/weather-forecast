using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

namespace WeatherForecasting.Infrastructure.Decorators;

public class ServiceClientLoggingDecorator : ServiceClientDecorator
{
    private readonly ILogger<ServiceClientLoggingDecorator> _logger;

    public ServiceClientLoggingDecorator(IWeatherServiceClient weatherServiceClient, ILogger<ServiceClientLoggingDecorator> logger)
        : base(weatherServiceClient)
    {
        _logger = logger;
    }
    
    public ServiceClientLoggingDecorator(IGeocodingServiceClient geocodingServiceClient, ILogger<ServiceClientLoggingDecorator> logger)
        : base(geocodingServiceClient)
    {
        _logger = logger;
    }

    public override async Task<WeatherForecast> GetWeatherForecastByCityAsync(string city, string country)
    {
        _logger.LogInformation("Requesting weather for {City} at {Country}", city, country);

        try
        {
            var result = await _weatherServiceClient.GetWeatherForecastByCityAsync(city, country);

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
            var result = await _weatherServiceClient.GetWeatherForecastByLonAndLanAsync(lon, lat);

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
            var result = await _weatherServiceClient.GetFiveDayForecastAsync(lon, lat);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting weather for {long} and {lat}", lon, lat);
            throw;
        }    
    }

    public override async Task<WeatherForecastForFiveDays> GetFiveDayForecastAsync(string city, string countryCode)
    {
        _logger.LogInformation("Requesting weather for {city} and {countryCode}", city, countryCode);

        try
        {
            var result = await _weatherServiceClient.GetFiveDayForecastAsync(city, countryCode);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting weather for {city} and {countryCode}", city, countryCode);
            throw;
        }       
    }

    public override async Task<Geolocation> ResolveCoordinatesAsync(string city, string countryCode)
    {
        _logger.LogInformation("Requesting location for {city} and {countryCode}", city, countryCode);

        try
        {
            var result = await _geocodingServiceClient.ResolveCoordinatesAsync(city, countryCode);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting weather for {city} and {countryCode}", city, countryCode);
            throw;
        }
    }
}