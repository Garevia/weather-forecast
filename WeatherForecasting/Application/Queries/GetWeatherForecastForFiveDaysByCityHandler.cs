using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Common;

namespace WeatherForecasting.Application.Queries;

/// <summary>
/// Handles the retrieval of a 5-day weather forecast for a specified city and country,
/// using the selected weather provider.
/// Processes a <see cref="GetWeatherForecastForFiveDaysByCityQuery"/> and returns a 
/// <see cref="Result{WeatherForecastForFiveDaysDto}"/> containing the forecast data.
/// </summary>
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