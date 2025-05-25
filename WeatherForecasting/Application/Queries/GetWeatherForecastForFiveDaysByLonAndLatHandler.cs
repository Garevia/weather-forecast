using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Common;

namespace WeatherForecasting.Application.Queries;

/// <summary>
/// Handles the retrieval of a 5-day weather forecast based on geographic coordinates (longitude and latitude),
/// using the specified weather provider.
/// Processes a <see cref="GetWeatherForecastForFiveDaysByLonAndLatQuery"/> and returns a 
/// <see cref="Result{WeatherForecastForFiveDaysDto}"/> containing the forecast data.
/// </summary>
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