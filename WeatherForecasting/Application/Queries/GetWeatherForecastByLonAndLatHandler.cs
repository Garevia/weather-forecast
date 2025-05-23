using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Infrastructure.Adapters;

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
        
        return OpenWeatherAdapter.ToDomain(response);;
    }
}