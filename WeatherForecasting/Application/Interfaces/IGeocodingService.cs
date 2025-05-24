using WeatherForecasting.Common;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Interfaces;

public interface IGeocodingService
{
    Task<Result<Geolocation>> ResolveCoordinatesAsync(string city, string countryCode, WeatherProviderType provider);
}