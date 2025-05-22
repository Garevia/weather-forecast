using WeatherForecasting.Infrastructure.WeatherProviders.Models;

namespace WeatherForecasting.Application.Interfaces;

public interface IOpenWeatherGeocodingService
{
    Task<OpenWeatherGeolocation?> ResolveCoordinatesAsync(string city, string countryCode);
}