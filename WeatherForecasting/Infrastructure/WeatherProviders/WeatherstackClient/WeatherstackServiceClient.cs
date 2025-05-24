using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient;

public class WeatherstackServiceClient : IWeatherServiceClient
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

    public Task<WeatherForecastForFiveDays> GetFiveDayForecastAsync(string city, string country)
    {
        throw new NotImplementedException();
    }
}