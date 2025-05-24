using WeatherForecasting.Domain.Enums;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Application.Interfaces;

public interface IWeatherServiceFactory
{ 
    IWeatherServiceClient CreateWeatherServiceClient(WeatherProvider provider);
}