using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Mappers;
using WeatherForecasting.Application.Weather.DTO;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastForFiveDaysByLonAndLatHandler : IRequestHandler<GetWeatherForecastForFiveDaysByLonAndLatQuery, WeatherForecastForFiveDaysDto>
{
    private readonly IWeatherServiceFactory _weatherServiceFactory;

    public GetWeatherForecastForFiveDaysByLonAndLatHandler(IWeatherServiceFactory weatherServiceFactory)
    {
        _weatherServiceFactory = weatherServiceFactory;
    }
    
    public async Task<WeatherForecastForFiveDaysDto> Handle(GetWeatherForecastForFiveDaysByLonAndLatQuery request, CancellationToken cancellationToken)
    {
        var weatherService = _weatherServiceFactory.GetWeatherServiceClient(request.Provider);

        var response = await weatherService.GetFiveDayForecastByLonAndLatAsync(request.Longitude, request.Latitude);
        
        return OpenWeatherMapper.ToDomain(response);;
    }
}