using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Mappers;
using WeatherForecasting.Application.Weather.DTO;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastForFiveDaysByLonAndLatHandler : IRequestHandler<GetWeatherForecastForFiveDaysByLonAndLatQuery, WeatherForecastForFiveDaysDto>
{
    private readonly IWeatherService _weatherService;

    public GetWeatherForecastForFiveDaysByLonAndLatHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }
    
    public async Task<WeatherForecastForFiveDaysDto> Handle(GetWeatherForecastForFiveDaysByLonAndLatQuery request, CancellationToken cancellationToken)
    {
        var response = await _weatherService.GetFiveDayForecastsAsync(request.lon, request.lat, request.Provider);
        
        return OpenWeatherMapper.ToDomain(response);;
    }
}