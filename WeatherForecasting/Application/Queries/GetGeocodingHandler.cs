using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Mappers;
using WeatherForecasting.Application.Weather.DTO;

namespace WeatherForecasting.Application.Queries;

public class GetGeocodingHandler : IRequestHandler<GetGeocodingQuery, GeolocationDto>
{
    private readonly IGeocodingService _geocodingService;

    public GetGeocodingHandler(IGeocodingService geocodingService)
    {
        _geocodingService = geocodingService;
    }

    public async Task<GeolocationDto> Handle(GetGeocodingQuery request, CancellationToken cancellationToken)
    {
        var response = await _geocodingService.ResolveCoordinatesAsync(request.City, request.Country, request.Provider);
        
        return OpenWeatherMapper.ToDomain(response);;
    }
}