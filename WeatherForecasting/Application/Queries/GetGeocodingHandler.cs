using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Common;

namespace WeatherForecasting.Application.Queries;

public class GetGeocodingHandler : IRequestHandler<GetGeocodingQuery, Result<GeolocationDto>>
{
    private readonly IGeolocationServiceFactory _geocodingServiceFactory;

    public GetGeocodingHandler(IGeolocationServiceFactory geocodingServiceFactory)
    {
        _geocodingServiceFactory = geocodingServiceFactory;
    }

    public async Task<Result<GeolocationDto>> Handle(GetGeocodingQuery request, CancellationToken cancellationToken)
    {
        var geocodingService = _geocodingServiceFactory.GetGeolocationServiceClient(request.Provider);
        var result = await geocodingService.ResolveCoordinatesAsync(request.City, request.CountryCode);

        if (!result.IsSuccess)
            return Result<GeolocationDto>.Failure(result.Error);

        var location = result.Value;

        return Result<GeolocationDto>.Success(new GeolocationDto()
        {
            Longitude = location.Longitude,
            Latitude = location.Latitude
        });
    }
}