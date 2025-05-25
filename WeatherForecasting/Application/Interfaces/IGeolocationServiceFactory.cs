using WeatherForecasting.Domain.Enums;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Application.Interfaces;

/// <summary>
/// Interface for implementing factory patter for different GeolocationServices
/// </summary>
public interface IGeolocationServiceFactory
{
    /// <summary>
    /// Getting geolocation service by provider
    /// </summary>
    /// <param name="provider">Provider name</param>
    /// <returns>Service instance</returns>
    IGeocodingServiceClient GetGeolocationServiceClient(WeatherProviderType provider);
}