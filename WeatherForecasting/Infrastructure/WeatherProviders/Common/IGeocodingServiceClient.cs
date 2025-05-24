using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.DTO;

namespace WeatherForecasting.Infrastructure.WeatherProviders.Common;

public interface IGeocodingServiceClient
{
    Task<Result<GeolocationDto>> ResolveCoordinatesAsync(string city, string countryCode);
}