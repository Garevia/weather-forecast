using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Interfaces;

public interface IWeatherServiceFactory
{ 
    IWeatherService Create(WeatherProvider provider);
}