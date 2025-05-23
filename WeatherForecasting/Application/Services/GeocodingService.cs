using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Services;

public class GeocodingService : IGeocodingService
{
    private readonly IGeolocationServiceFactory _geolocationServiceFactory;

    public GeocodingService(IGeolocationServiceFactory geolocationServiceFactory)
    {
        this._geolocationServiceFactory = geolocationServiceFactory;
    }
    
    public async Task<Geolocation> ResolveCoordinatesAsync(string city, string countryCode, WeatherProvider provider)
    {
        var geolocationProvider = _geolocationServiceFactory.CreateGeolocationServiceClient(provider);
        return await geolocationProvider.ResolveCoordinatesAsync(city, countryCode);
    }
}