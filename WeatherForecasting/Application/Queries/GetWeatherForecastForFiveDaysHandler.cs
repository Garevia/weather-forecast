using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Infrastructure.Adapters;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastForFiveDaysHandler  : IRequestHandler<GetWeatherForecastForFiveDaysQuery, WeatherForecastForFiveDaysDto>
{
    private readonly IWeatherServiceFactory _serviceFactory;

    public GetWeatherForecastForFiveDaysHandler(IWeatherServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }
    
    public async Task<WeatherForecastForFiveDaysDto> Handle(GetWeatherForecastForFiveDaysQuery request, CancellationToken cancellationToken)
    {
        var serviceProvider = _serviceFactory.Create(request.Provider);
        var response = await serviceProvider.GetFiveDayForecastAsync(request.lon, request.lat);
        
        return OpenWeatherAdapter.ToDomain(response);;
    }
}