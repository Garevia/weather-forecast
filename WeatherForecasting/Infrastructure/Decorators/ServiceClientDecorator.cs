using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

namespace WeatherForecasting.Infrastructure.Decorators;

public abstract class ServiceClientDecorator : IWeatherServiceClient, IGeocodingServiceClient
{
    protected readonly IWeatherServiceClient _weatherServiceClient;
    protected readonly IGeocodingServiceClient _geocodingServiceClient;
    
    protected ServiceClientDecorator(IWeatherServiceClient weatherServiceClient)
    {
        _weatherServiceClient = weatherServiceClient;
    }
    
    protected ServiceClientDecorator(IGeocodingServiceClient geocodingServiceClient)
    {
        _geocodingServiceClient = geocodingServiceClient;
    }

    public abstract Task<WeatherForecast> GetWeatherForecastByCityAsync(string city, string country);
    
    public abstract Task<WeatherForecast> GetWeatherForecastByLonAndLanAsync(double lon, double lat);
    
    public abstract Task<WeatherForecastForFiveDays> GetFiveDayForecastAsync(double lon, double lat);
    
    public abstract Task<Geolocation> ResolveCoordinatesAsync(string city, string countryCode);
}
