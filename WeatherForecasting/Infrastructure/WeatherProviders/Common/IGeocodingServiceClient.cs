using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.DTO;

namespace WeatherForecasting.Infrastructure.WeatherProviders.Common;

/// <summary>
/// IGeocodingServiceClient decorator interface that adds caching functionality to it before execution
/// </summary>
public interface IGeocodingServiceClient
{
    Task<Result<GeolocationDto>> ResolveCoordinatesAsync(string city, string countryCode);
}