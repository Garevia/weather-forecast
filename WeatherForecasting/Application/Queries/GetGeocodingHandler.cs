using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Mappers;
using WeatherForecasting.Application.Weather.DTO;

namespace WeatherForecasting.Application.Queries;

public class GetGeocodingHandler : IRequestHandler<GetGeocodingQuery, GeolocationDto>
{
    private readonly IGeolocationServiceFactory _geocodingServiceFactory;

    public GetGeocodingHandler(IGeolocationServiceFactory geocodingServiceFactory)
    {
        _geocodingServiceFactory = geocodingServiceFactory;
    }

    public async Task<GeolocationDto> Handle(GetGeocodingQuery request, CancellationToken cancellationToken)
    {
        var geocodingService = _geocodingServiceFactory.GetGeolocationServiceClient(request.Provider);
        var response = await geocodingService.ResolveCoordinatesAsync(request.City, request.CountryCode);
        
        return OpenWeatherMapper.ToDomain(response);;
    }
}