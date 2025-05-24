using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.DTO;

namespace WeatherForecasting.Infrastructure.WeatherProviders.Common;

public interface IWeatherServiceClient
{
    Task<Result<WeatherDto>> GetWeatherForecastByCityAsync(string city, string country);
    
    Task<Result<WeatherDto>> GetWeatherForecastByLonAndLanAsync(double lon, double lat);
    
    Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByLonAndLatAsync(double longitude, double latitude);
    
    Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByCityAsync(string city, string countryCode);
}