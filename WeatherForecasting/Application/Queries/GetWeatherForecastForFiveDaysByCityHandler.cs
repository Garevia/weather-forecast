using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Mappers;
using WeatherForecasting.Application.Weather.DTO;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastForFiveDaysByCityHandler : IRequestHandler<GetWeatherForecastForFiveDaysByCityQuery,
    WeatherForecastForFiveDaysDto>
{
    private readonly IWeatherServiceFactory _weatherServiceFactory;

    public GetWeatherForecastForFiveDaysByCityHandler(IWeatherServiceFactory weatherServiceFactory)
    {
        _weatherServiceFactory = weatherServiceFactory;
    }

    public async Task<WeatherForecastForFiveDaysDto> Handle(GetWeatherForecastForFiveDaysByCityQuery request,
        CancellationToken cancellationToken)
    {
        var weatherService = _weatherServiceFactory.GetWeatherServiceClient(request.Provider);

        var response = await weatherService.GetFiveDayForecastByCityAsync(request.City, request.CountryCode);

        return OpenWeatherMapper.ToDomain(response);
    }
}