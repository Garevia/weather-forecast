using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Services;

public class GeocodingService : IGeocodingService
{
    private readonly IGeolocationServiceFactory _serviceFactory;

    public GeocodingService(IGeolocationServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }
    
    public async Task<Result<Geolocation>> ResolveCoordinatesAsync(string city, string countryCode, WeatherProviderType provider)
    {
        var service = _serviceFactory.GetGeolocationServiceClient(provider);
        
        var dtoResult = await service.ResolveCoordinatesAsync(city, countryCode);

        if (!dtoResult.IsSuccess)
            return Result<Geolocation>.Failure(dtoResult.Error.Message, dtoResult.Error.HttpStatusCode);

        var dto = dtoResult.Value;
        var forecast = new Geolocation(dtoResult.Value.Latitude, dtoResult.Value.Longitude);

        return Result<Geolocation>.Success(forecast);
    }
}