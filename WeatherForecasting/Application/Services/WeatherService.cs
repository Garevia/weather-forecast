using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherServiceFactory _serviceFactory;

    public WeatherService(IWeatherServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public async Task<Result<WeatherForecast>> GetWeatherForecastByCityAsync(string city, string countryCode, WeatherProviderType provider)
    {
        var service = _serviceFactory.GetWeatherServiceClient(provider);
        var dtoResult = await service.GetWeatherForecastByCityAsync(city, countryCode);

        if (!dtoResult.IsSuccess)
            return Result<WeatherForecast>.Failure(dtoResult.Error);

        var dto = dtoResult.Value;
        var forecast = new WeatherForecast(dto.City, dto.CountryCode, dto.Description, dto.TemperatureCelsius, dto.Date);

        return Result<WeatherForecast>.Success(forecast);
    }

    public async Task<Result<WeatherForecast>> GetWeatherForecastByLonAndLanAsync(double longitude, double latitude, WeatherProviderType provider)
    {
        var service = _serviceFactory.GetWeatherServiceClient(provider);
        var dtoResult = await service.GetWeatherForecastByLonAndLanAsync(longitude, latitude);

        if (!dtoResult.IsSuccess)
            return Result<WeatherForecast>.Failure(dtoResult.Error);

        var dto = dtoResult.Value;
        var forecast = new WeatherForecast(dto.City, dto.CountryCode, dto.Description, dto.TemperatureCelsius, dto.Date);

        return Result<WeatherForecast>.Success(forecast);
    }

    public async Task<Result<WeatherForecastForFiveDays>> GetFiveDayForecastsByLonAndLanAsync(double longitude, double latitude, WeatherProviderType provider)
    {
        var service = _serviceFactory.GetWeatherServiceClient(provider);
        var dtoResult = await service.GetFiveDayForecastByLonAndLatAsync(longitude, latitude);

        if (!dtoResult.IsSuccess)
            return Result<WeatherForecastForFiveDays>.Failure(dtoResult.Error);

        var dto = dtoResult.Value;
        var forecast = new WeatherForecastForFiveDays()
        {
            City = dto.City,
            CountryCode = dto.CountryCode,
            Forecasts = dto.Forecasts.Select(x =>
                new WeatherForecast(dto.City, dto.CountryCode, x.Description, x.TemperatureCelsius, x.Date))
        };

        return Result<WeatherForecastForFiveDays>.Success(forecast);
    }

    public async Task<Result<WeatherForecastForFiveDays>> GetFiveDayForecastsByCityAsync(string city, string countryCode, WeatherProviderType provider)
    {
        var service = _serviceFactory.GetWeatherServiceClient(provider);
        var dtoResult = await service.GetFiveDayForecastByCityAsync(city, countryCode);

        if (!dtoResult.IsSuccess)
            return Result<WeatherForecastForFiveDays>.Failure(dtoResult.Error);

        var dto = dtoResult.Value;
        var forecast = new WeatherForecastForFiveDays()
        {
            City = dto.City,
            CountryCode = dto.CountryCode,
            Forecasts = dto.Forecasts.Select(x =>
                new WeatherForecast(dto.City, dto.CountryCode, x.Description, x.TemperatureCelsius, x.Date))
        };

        return Result<WeatherForecastForFiveDays>.Success(forecast);
    }
}