using WeatherForecasting.Domain.Enums;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Application.Interfaces;

public interface IGeolocationServiceFactory
{
    IGeocodingServiceClient GetGeolocationServiceClient(WeatherProviderType provider);
}