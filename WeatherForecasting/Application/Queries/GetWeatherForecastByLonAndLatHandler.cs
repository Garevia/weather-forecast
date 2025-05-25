using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Common;

namespace WeatherForecasting.Application.Queries;

/// <summary>
/// Handles the retrieval of weather forecast data based on geographic coordinates (longitude and latitude).
/// Processes a <see cref="GetWeatherForecastByLonAndLatQuery"/> and returns a 
/// <see cref="Result{WeatherForecastDto}"/> containing the forecast information.
/// </summary>
public class GetWeatherForecastByLonAndLatHandler : IRequestHandler<GetWeatherForecastByLonAndLatQuery, Result<WeatherForecastDto>>
{
    private readonly IWeatherService _weatherService;

    public GetWeatherForecastByLonAndLatHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }
    public async Task<Result<WeatherForecastDto>> Handle(GetWeatherForecastByLonAndLatQuery request, CancellationToken cancellationToken)
    {
        var result = await _weatherService.GetWeatherForecastByLonAndLanAsync(request.Longitude, request.Latitude, request.Provider);

        if (!result.IsSuccess)
            return Result<WeatherForecastDto>.Failure(result.Error.Message, result.Error.HttpStatusCode);

        var forecast = result.Value;

        return Result<WeatherForecastDto>.Success(new WeatherForecastDto
        {
            City = forecast.City,
            CountryCode = forecast.CountryCode,
            TemperatureCelsius = forecast.TemperatureCelsius,
            DateTime = forecast.DateTime,
            WeatherProvider = request.Provider,
            Description = forecast.Description
        });
    }
}