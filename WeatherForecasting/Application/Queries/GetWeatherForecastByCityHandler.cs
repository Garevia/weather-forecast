using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Common;

namespace WeatherForecasting.Application.Queries;

public class GetWeatherForecastByCityHandler : IRequestHandler<GetWeatherForecastByCityQuery, Result<WeatherForecastDto>>
{
    private readonly IWeatherService _weatherService;

    public GetWeatherForecastByCityHandler(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    public async Task<Result<WeatherForecastDto>> Handle(GetWeatherForecastByCityQuery request, CancellationToken cancellationToken)
    {
        var result = await _weatherService.GetWeatherForecastByCityAsync(request.City, request.CountryCode, request.Provider);

        if (!result.IsSuccess)
            return Result<WeatherForecastDto>.Failure(result.Error);

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