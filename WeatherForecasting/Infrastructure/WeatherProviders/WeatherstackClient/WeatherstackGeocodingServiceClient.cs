using System.Net;
using Microsoft.Extensions.Options;
using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.DTO;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Models;

namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient;

public class WeatherstackGeocodingServiceClient : IGeocodingServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherstackGeocodingServiceClient(HttpClient httpClient, IOptions<WeatherstackWeatherApiOptions> options)
    {
        _httpClient = httpClient;
        _apiKey = options.Value.ApiKey 
                  ?? throw new ArgumentNullException("Weatherstack API key is not configured");
    }

    public Task<Result<GeolocationDto>> ResolveCoordinatesAsync(string city, string countryCode)
    {
        return Task.FromResult(Result<GeolocationDto>.Failure("Method not supported for provider WeatherStack",
            HttpStatusCode.Forbidden));
    }
}