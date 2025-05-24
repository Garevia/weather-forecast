using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Mappers;
using WeatherForecasting.Application.Weather.DTO;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastByLonAndLatHandler : IRequestHandler<GetWeatherForecastByLonAndLatQuery, WeatherForecastDto>
{
    private readonly IWeatherServiceFactory _weatherServiceFactory;

    public GetWeatherForecastByLonAndLatHandler(IWeatherServiceFactory weatherServiceFactory)
    {
        _weatherServiceFactory = weatherServiceFactory;
    }

    public async Task<WeatherForecastDto> Handle(GetWeatherForecastByLonAndLatQuery request, CancellationToken cancellationToken)
    {
        var weatherService = _weatherServiceFactory.GetWeatherServiceClient(request.Provider);

        var response = await weatherService.GetWeatherForecastByLonAndLanAsync(request.Longitude, request.Latitude);
        
        return OpenWeatherMapper.ToDomain(response);;
    }
}