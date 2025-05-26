using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;
using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.DTO;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;

public class OpenWeatherGeocodingServiceClient : IGeocodingServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenWeatherGeocodingServiceClient(HttpClient httpClient, IOptions<OpenMapWeatherApiOptions> options)
    {
        _httpClient = httpClient;
        _apiKey = options.Value.ApiKey 
                  ?? throw new ArgumentNullException("OpenWeatherMap API key is not configured");
    }

    public async Task<Result<GeolocationDto>> ResolveCoordinatesAsync(string city, string countryCode)
    {
        try
        {
            var url = string.Format(OpenWeatherApiEndpoints.GeocodingDirect, city, countryCode,
                _apiKey);

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return Result<GeolocationDto>.Failure($"Error in getting data, error code {response.StatusCode}", response.StatusCode);
            }
            var content = await response.Content.ReadAsStringAsync();
            
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
            };
            
            var locations = JsonSerializer.Deserialize<List<OpenWeatherGeolocationResponse>>(content, options);

            if (locations is null || !locations.Any())
            {
                return Result<GeolocationDto>.Failure("Error in getting data");
            }
            
            var location = new GeolocationDto(Math.Round(locations[0].Lat, 7), Math.Round(locations[0].Lon, 7));
            
            return Result<GeolocationDto>.Success(location);
        }
        catch (Exception ex)
        {
            return Result<GeolocationDto>.Failure("Unexpected error occurred: " + ex.Message);
        }
    }
}