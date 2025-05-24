using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.DTO;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;

namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient;

public class WeatherstackGeocodingServiceClient : IGeocodingServiceClient
{
    public Task<Result<GeolocationDto>> ResolveCoordinatesAsync(string city, string countryCode)
    {
        throw new NotImplementedException();
    }
}