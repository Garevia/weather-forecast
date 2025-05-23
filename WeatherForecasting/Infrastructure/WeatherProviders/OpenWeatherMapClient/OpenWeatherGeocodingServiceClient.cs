using System.Text.Json;
using Microsoft.Extensions.Options;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;

public class OpenWeatherGeocodingServiceClient : IGeocodingServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenWeatherGeocodingServiceClient(HttpClient httpClient, IOptions<WeatherApiOptions> options)
    {
        _httpClient = httpClient;
        _apiKey = options.Value.ApiKey 
                  ?? throw new ArgumentNullException("OpenWeatherMap API key is not configured");
    }

    public async Task<Geolocation> ResolveCoordinatesAsync(string city, string countryCode)
    {
        var url = string.Format(WeatherApiEndpoints.GeocodingDirect, city, countryCode,
            _apiKey);
        
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var locations = JsonSerializer.Deserialize<List<OpenWeatherGeolocation>>(json);
        return new Geolocation(locations[0].Lat, locations[0].Lon);
    }
}