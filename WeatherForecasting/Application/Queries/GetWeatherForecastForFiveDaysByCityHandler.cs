using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Common;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastForFiveDaysByCityHandler : IRequestHandler<GetWeatherForecastForFiveDaysByCityQuery,
    Result<WeatherForecastForFiveDaysDto>>
{
    private readonly IWeatherService _weatherService;

    public GetWeatherForecastForFiveDaysByCityHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<Result<WeatherForecastForFiveDaysDto>> Handle(GetWeatherForecastForFiveDaysByCityQuery request,
        CancellationToken cancellationToken)
    {
        var result =
            await _weatherService.GetFiveDayForecastsByCityAsync(request.City, request.CountryCode, request.Provider);

        if (!result.IsSuccess)
            return Result<WeatherForecastForFiveDaysDto>.Failure(result.Error);

        var forecast = result.Value;

        return Result<WeatherForecastForFiveDaysDto>.Success(new WeatherForecastForFiveDaysDto
        {
            City = forecast.City,
            CountryCode = forecast.CountryCode,
            Provider = request.Provider,
            Forecasts = forecast.Forecasts.Select(x => new WeatherForecastForTimeStampDto()
            {
                TemperatureCelsius = x.TemperatureCelsius,
                DateTime = x.DateTime,
                Description = x.Description
            })
        });
    }
}