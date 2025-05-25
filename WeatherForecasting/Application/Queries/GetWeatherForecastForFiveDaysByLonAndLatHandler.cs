using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Common;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastForFiveDaysByLonAndLatHandler : IRequestHandler<GetWeatherForecastForFiveDaysByLonAndLatQuery, Result<WeatherForecastForFiveDaysDto>>
{
    private readonly IWeatherService _weatherService;

    public GetWeatherForecastForFiveDaysByLonAndLatHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<Result<WeatherForecastForFiveDaysDto>> Handle(
        GetWeatherForecastForFiveDaysByLonAndLatQuery request, CancellationToken cancellationToken)
    {
        var result =
            await _weatherService.GetFiveDayForecastsByLonAndLanAsync(request.Longitude, request.Latitude,
                request.Provider);

        if (!result.IsSuccess)
            return Result<WeatherForecastForFiveDaysDto>.Failure(result.Error.Message, result.Error.HttpStatusCode);

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