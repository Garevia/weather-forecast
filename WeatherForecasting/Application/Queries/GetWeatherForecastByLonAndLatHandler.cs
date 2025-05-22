using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Infrastructure.Adapters;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastByLonAndLatHandler : IRequestHandler<GetWeatherForecastByLonAndLatQuery, WeatherForecastDto>
{
    private readonly IWeatherServiceFactory _serviceFactory;

    public GetWeatherForecastByLonAndLatHandler(IWeatherServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public async Task<WeatherForecastDto> Handle(GetWeatherForecastByLonAndLatQuery request, CancellationToken cancellationToken)
    {
        var serviceProvider = _serviceFactory.Create(request.Provider);
        var response = await serviceProvider.GetWeatherForecastByLonAndLanAsync(request.lon, request.lat);
        
        return OpenWeatherAdapter.ToDomain(response);;
    }
}