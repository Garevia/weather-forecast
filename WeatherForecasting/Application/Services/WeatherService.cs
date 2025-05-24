using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Application.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherServiceClient _weatherServiceClient;

    public WeatherService(IWeatherServiceClient weatherServiceClient)
    { 
        _weatherServiceClient = weatherServiceClient;
    }

    public async Task<WeatherForecast> GetWeatherForecastByCityAsync(string city, string country)
    {
        return await _weatherServiceClient.GetWeatherForecastByCityAsync(city, country);
    }

    public async Task<WeatherForecast> GetWeatherForecastByLonAndLanAsync(double longitude, double latitude)
    {
        return await _weatherServiceClient.GetWeatherForecastByLonAndLanAsync(longitude, latitude);
    }

    public async Task<WeatherForecastForFiveDays> GetFiveDayForecastsAsync(double longitude, double latitude)
    {
        return await _weatherServiceClient.GetFiveDayForecastByLonAndLatAsync(longitude, latitude);
    }

    public async Task<WeatherForecastForFiveDays> GetFiveDayForecastsAsync(string city, string countryCode)
    {
        return await _weatherServiceClient.GetFiveDayForecastByCityAsync(city, countryCode);
    }
}