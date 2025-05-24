using WeatherForecasting.Domain.Entities;

namespace WeatherForecasting.Infrastructure.WeatherProviders.Common;

public interface IGeocodingServiceClient
{
    Task<Geolocation> ResolveCoordinatesAsync(string city, string countryCode);
}