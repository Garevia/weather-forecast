using WeatherForecasting.Common;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Interfaces;

/// <summary>
/// Service for getting location data
/// </summary>
public interface IGeocodingService
{
    /// <summary>
    /// Resolves Coordinates of the city 
    /// </summary>
    /// <param name="city">City by string</param>
    /// <param name="countryCode">Country code letters</param>
    /// <param name="provider">Provider Name</param>
    /// <returns>Longitude and Latitude</returns>
    Task<Result<Geolocation>> ResolveCoordinatesAsync(string city, string countryCode, WeatherProviderType provider);
}