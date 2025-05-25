using WeatherForecasting.Domain.Enums;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Application.Interfaces;

/// <summary>
/// Interface for implementing factory patter for different GeolocationServices
/// </summary>
public interface IWeatherServiceFactory
{ 
    /// <summary>
    /// Method for getting appropriate service client
    /// </summary>
    /// <param name="provider">Provider name</param>
    /// <returns>Service client instance for the provider</returns>
    IWeatherServiceClient GetWeatherServiceClient(WeatherProviderType provider);
}