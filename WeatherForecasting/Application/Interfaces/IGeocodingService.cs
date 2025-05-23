using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Interfaces;

public interface IGeocodingService
{
    Task<Geolocation> ResolveCoordinatesAsync(string city, string countryCode, WeatherProvider provider);
}