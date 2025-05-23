using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Mappers;
using WeatherForecasting.Application.Weather.DTO;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastByCityHandler : IRequestHandler<GetWeatherForecastByCityQuery, WeatherForecastDto>
{
    private readonly IWeatherService _weatherService;

    public GetWeatherForecastByCityHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<WeatherForecastDto> Handle(GetWeatherForecastByCityQuery request, CancellationToken cancellationToken)
    {
        var response = await _weatherService.GetWeatherForecastByCityAsync(request.City, request.Country, request.Provider);
        
        return OpenWeatherMapper.ToDomain(response);;
    }
}