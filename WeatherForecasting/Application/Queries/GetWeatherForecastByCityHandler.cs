using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Infrastructure.Adapters;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastByCityHandler : IRequestHandler<GetWeatherForecastByCityQuery, WeatherForecastDto>
{
    private readonly IWeatherServiceFactory _serviceFactory;

    public GetWeatherForecastByCityHandler(IWeatherServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public async Task<WeatherForecastDto> Handle(GetWeatherForecastByCityQuery request, CancellationToken cancellationToken)
    {
        var serviceProvider = _serviceFactory.Create(request.Provider);
        var response = await serviceProvider.GetWeatherForecastByCityAsync(request.City, request.Country);
        
        return OpenWeatherAdapter.ToDomain(response);;
    }
}