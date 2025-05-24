using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Mappers;
using WeatherForecasting.Application.Weather.DTO;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastByCityHandler : IRequestHandler<GetWeatherForecastByCityQuery, WeatherForecastDto>
{
    private readonly IWeatherServiceFactory _weatherServiceFactory;

    public GetWeatherForecastByCityHandler(IWeatherServiceFactory weatherServiceFactory)
    {
        _weatherServiceFactory = weatherServiceFactory;
    }

    public async Task<WeatherForecastDto> Handle(GetWeatherForecastByCityQuery request, CancellationToken cancellationToken)
    {
        var weatherService = _weatherServiceFactory.GetWeatherServiceClient(request.Provider);
        var response = await weatherService.GetWeatherForecastByCityAsync(request.City, request.CountryCode);
        
        return OpenWeatherMapper.ToDomain(response);;
    }
}