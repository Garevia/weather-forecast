using WeatherForecasting.Domain.Entities;

namespace WeatherForecasting.Application.Interfaces;

public interface IGeocodingService
{
    Task<Geolocation> ResolveCoordinatesAsync(string city, string countryCode);
}