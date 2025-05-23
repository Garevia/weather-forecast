using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient;

public class WeatherstackGeocodingServiceClient : IGeocodingServiceClient
{
    public Task<Geolocation> ResolveCoordinatesAsync(string city, string countryCode)
    {
        throw new NotImplementedException();
    }
}