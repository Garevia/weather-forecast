using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.DTO;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Infrastructure.Decorators;

public class WeatherServiceClientLoggingDecorator : IWeatherServiceClient
{
    private readonly IWeatherServiceClient _weatherServiceClient;
    private readonly ILogger<WeatherServiceClientLoggingDecorator> _logger;

    public WeatherServiceClientLoggingDecorator(
        IWeatherServiceClient weatherServiceClient,
        ILogger<WeatherServiceClientLoggingDecorator> logger)
    {
        _weatherServiceClient = weatherServiceClient;
        _logger = logger;
    }

    public async Task<Result<WeatherDto>> GetWeatherForecastByCityAsync(string city, string country)
    {
        _logger.LogInformation("Requesting weather for {City} at {Country}", city, country);

        try
        {
            var result = await _weatherServiceClient.GetWeatherForecastByCityAsync(city, country);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting weather for {City}", city);
            throw;
        }
    }

    public  async Task<Result<WeatherDto>> GetWeatherForecastByLonAndLanAsync(double lon, double lat)
    {
        _logger.LogInformation("Requesting weather for {long} and {lat}", lon, lat);

        try
        {
            var result = await _weatherServiceClient.GetWeatherForecastByLonAndLanAsync(lon, lat);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting weather for {long} and {lat}", lon, lat);
            throw;
        }
    }

    public  async Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByLonAndLatAsync(double lon, double lat)
    {
        _logger.LogInformation("Requesting weather for {long} and {lat}", lon, lat);

        try
        {
            var result = await _weatherServiceClient.GetFiveDayForecastByLonAndLatAsync(lon, lat);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting weather for {long} and {lat}", lon, lat);
            throw;
        }    
    }

    public async Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByCityAsync(string city, string countryCode)
    {
        _logger.LogInformation("Requesting weather for {city} and {countryCode}", city, countryCode);

        try
        {
            var result = await _weatherServiceClient.GetFiveDayForecastByCityAsync(city, countryCode);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting weather for {city} and {countryCode}", city, countryCode);
            throw;
        }       
    }
}