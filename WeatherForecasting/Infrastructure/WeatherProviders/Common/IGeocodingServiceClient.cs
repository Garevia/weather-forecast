using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.DTO;

namespace WeatherForecasting.Infrastructure.WeatherProviders.Common;

/// <summary>
/// IGeocodingServiceClient interface that implements 3rd party location APIs
/// </summary>
public interface IGeocodingServiceClient
{
    /// <summary>
    /// Resolves the geographic coordinates (latitude and longitude) for the specified city and country code.
    /// </summary>
    /// <param name="city">The name of the city to resolve.</param>
    /// <param name="countryCode">The country code (e.g., "US").</param>
    /// <returns>A <see cref="Result{GeolocationDto}"/> containing the coordinates or error information.</returns>
    Task<Result<GeolocationDto>> ResolveCoordinatesAsync(string city, string countryCode);
}