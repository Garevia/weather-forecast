using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Application.Services;

public class GeocodingService : IGeocodingService
{
    private readonly IGeocodingServiceClient _geocodingServiceClient;

    public GeocodingService(IGeocodingServiceClient geocodingServiceClient)
    {
        this._geocodingServiceClient = geocodingServiceClient;
    }
    
    public async Task<Geolocation> ResolveCoordinatesAsync(string city, string countryCode)
    {
        return await _geocodingServiceClient.ResolveCoordinatesAsync(city, countryCode);
    }
}