using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Infrastructure.Adapters;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastForFiveDaysHandler  : IRequestHandler<GetWeatherForecastForFiveDaysQuery, WeatherForecastForFiveDaysDto>
{
    private readonly IWeatherService _weatherService;

    public GetWeatherForecastForFiveDaysHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }
    
    public async Task<WeatherForecastForFiveDaysDto> Handle(GetWeatherForecastForFiveDaysQuery request, CancellationToken cancellationToken)
    {
        var response = await _weatherService.GetFiveDayForecastsAsync(request.lon, request.lat, request.Provider);
        
        return OpenWeatherAdapter.ToDomain(response);;
    }
}