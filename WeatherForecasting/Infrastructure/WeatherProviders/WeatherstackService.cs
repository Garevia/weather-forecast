using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Domain.Entities;

namespace WeatherForecasting.Infrastructure.WeatherProviders;

public class WeatherstackService : IWeatherService
{
    public Task<WeatherForecast> GetWeatherForecastByCityAsync(string city, string country)
    {
        throw new NotImplementedException();
    }

    public Task<WeatherForecast> GetWeatherForecastByLonAndLanAsync(double lon, double lan)
    {
        throw new NotImplementedException();
    }

    public Task<WeatherForecastForFiveDays> GetFiveDayForecastAsync(double lon, double lat)
    {
        throw new NotImplementedException();
    }
}