using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

namespace WeatherForecasting.Infrastructure.WeatherProviders.Common;

public interface IGeocodingServiceClient
{
    Task<Geolocation> ResolveCoordinatesAsync(string city, string countryCode);
}