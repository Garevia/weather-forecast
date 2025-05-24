using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.DTO;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient;

public class WeatherstackServiceClient : IWeatherServiceClient
{
    public Task<Result<WeatherDto>> GetWeatherForecastByCityAsync(string city, string country)
    {
        throw new NotImplementedException();
    }

    public Task<Result<WeatherDto>> GetWeatherForecastByLonAndLanAsync(double lon, double lat)
    {
        throw new NotImplementedException();
    }

    public Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByLonAndLatAsync(double lon, double lat)
    {
        throw new NotImplementedException();
    }

    public Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByCityAsync(string city, string countryCode)
    {
        throw new NotImplementedException();
    }
}