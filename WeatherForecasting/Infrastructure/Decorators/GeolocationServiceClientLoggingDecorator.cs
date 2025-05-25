using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.DTO;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Infrastructure.Decorators;

public class GeolocationServiceClientLoggingDecorator : IGeocodingServiceClient
{
    private readonly IGeocodingServiceClient _geocodingServiceClient;
    private readonly ILogger<GeolocationServiceClientLoggingDecorator> _logger;

    public GeolocationServiceClientLoggingDecorator(
        IGeocodingServiceClient geocodingServiceClient,
        ILogger<GeolocationServiceClientLoggingDecorator> logger)
    {
        _geocodingServiceClient = geocodingServiceClient;
        _logger = logger;
    }
    
    public async Task<Result<GeolocationDto>> ResolveCoordinatesAsync(string city, string countryCode)
    {
        _logger.LogInformation("Requesting location for {City} at {Country}", city, countryCode);

        try
        {
            var result = await _geocodingServiceClient.ResolveCoordinatesAsync(city, countryCode);

            _logger.LogInformation("Location received: {city}°C, {countryCode}", city, countryCode);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting location for {City}", city);
            throw;
        }
    }
}