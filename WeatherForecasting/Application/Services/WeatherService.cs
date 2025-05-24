using StackExchange.Redis;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Services;

public class WeatherService : IWeatherService
{
    private readonly IWeatherServiceFactory _weatherServiceFactory;
    private readonly IDatabase _redisDb;

    public WeatherService(IWeatherServiceFactory weatherServiceFactory, ConnectionMultiplexer redis)
    {
        _weatherServiceFactory = weatherServiceFactory;
        _redisDb = redis.GetDatabase();
    }

    public async Task<WeatherForecast> GetWeatherForecastByCityAsync(string city, string country,
        WeatherProvider provider)
    {
        var weatherProvider = _weatherServiceFactory.CreateWeatherServiceClient(provider);
        return await weatherProvider.GetWeatherForecastByCityAsync(city, country);
    }

    public async Task<WeatherForecast> GetWeatherForecastByLonAndLanAsync(double lon, double lat,
        WeatherProvider provider)
    {
        var weatherProvider = _weatherServiceFactory.CreateWeatherServiceClient(provider);
        return await weatherProvider.GetWeatherForecastByLonAndLanAsync(lon, lat);
    }

    public async Task<WeatherForecastForFiveDays> GetFiveDayForecastsAsync(double lon, double lat,
        WeatherProvider provider)
    {

        var weatherProvider = _weatherServiceFactory.CreateWeatherServiceClient(provider);
        return await weatherProvider.GetFiveDayForecastAsync(lon, lat);
    }

    public async Task<WeatherForecastForFiveDays> GetFiveDayForecastsAsync(string city, string country, WeatherProvider provider)
    {
        var weatherProvider = _weatherServiceFactory.CreateWeatherServiceClient(provider);
        return await weatherProvider.GetFiveDayForecastAsync(city, country);
    }
}