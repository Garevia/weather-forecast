using WeatherForecasting.Domain.Enums;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;

namespace WeatherForecasting.Application.Interfaces;

public interface IWeatherServiceFactory
{ 
    IWeatherServiceClient CreateWeatherServiceClient(WeatherProvider provider);
}