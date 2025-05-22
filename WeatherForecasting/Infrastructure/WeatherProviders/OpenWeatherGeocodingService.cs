using System.Text.Json;
using Microsoft.Extensions.Options;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Infrastructure.WeatherProviders.Models;

namespace WeatherForecasting.Infrastructure.WeatherProviders;

public class OpenWeatherGeocodingService : IOpenWeatherGeocodingService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenWeatherGeocodingService(HttpClient httpClient, IOptions<WeatherApiOptions> options)
    {
        _httpClient = httpClient;
        _apiKey = options.Value.ApiKey 
                  ?? throw new ArgumentNullException("OpenWeatherMap API key is not configured");
    }

    public async Task<OpenWeatherGeolocation?> ResolveCoordinatesAsync(string city, string countryCode)
    {
        var url = $"http://api.openweathermap.org/geo/1.0/direct?q={city},{countryCode}&limit=1&appid={_apiKey}";
        
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var locations = JsonSerializer.Deserialize<List<OpenWeatherGeolocation>>(json);
        return locations?.FirstOrDefault();
    }
}