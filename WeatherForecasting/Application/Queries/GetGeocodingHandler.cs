using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Infrastructure.Adapters;

namespace WeatherForecasting.Application.Queries;

public class GetGeocodingHandler : IRequestHandler<GetGeocodingQuery, GeolocationDto>
{
    private readonly IWeatherService _weatherService;

    public GetGeocodingHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<GeolocationDto> Handle(GetGeocodingQuery request, CancellationToken cancellationToken)
    {
        var response = await _weatherService.ResolveCoordinatesAsync(request.City, request.Country, request.Provider);
        
        return OpenWeatherAdapter.ToDomain(response);;
    }
}