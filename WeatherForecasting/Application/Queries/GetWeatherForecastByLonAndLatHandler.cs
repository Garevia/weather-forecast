using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Mappers;
using WeatherForecasting.Application.Weather.DTO;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastByLonAndLatHandler : IRequestHandler<GetWeatherForecastByLonAndLatQuery, WeatherForecastDto>
{
    private readonly IWeatherService _weatherService;

    public GetWeatherForecastByLonAndLatHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<WeatherForecastDto> Handle(GetWeatherForecastByLonAndLatQuery request, CancellationToken cancellationToken)
    {
        var response = await _weatherService.GetWeatherForecastByLonAndLanAsync(request.lon, request.lat, request.Provider);
        
        return OpenWeatherMapper.ToDomain(response);;
    }
}