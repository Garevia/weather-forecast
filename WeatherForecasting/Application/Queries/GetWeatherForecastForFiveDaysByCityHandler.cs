using MediatR;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Mappers;
using WeatherForecasting.Application.Weather.DTO;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastForFiveDaysByCityHandler : IRequestHandler<GetWeatherForecastForFiveDaysByCityQuery,
    WeatherForecastForFiveDaysDto>
{
    private readonly IWeatherService _weatherService;

    public GetWeatherForecastForFiveDaysByCityHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<WeatherForecastForFiveDaysDto> Handle(GetWeatherForecastForFiveDaysByCityQuery request,
        CancellationToken cancellationToken)
    {
        var response = await _weatherService.GetFiveDayForecastsAsync(request.City, request.Country, request.Provider);

        return OpenWeatherMapper.ToDomain(response);
    }
}