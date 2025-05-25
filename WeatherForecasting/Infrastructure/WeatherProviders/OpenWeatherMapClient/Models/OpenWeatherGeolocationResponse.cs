using System.Text.Json.Serialization;

namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

/// <summary>
/// Represents the geolocation response from the OpenWeather API,
/// containing latitude and longitude coordinates.
/// </summary>
public class OpenWeatherGeolocationResponse
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    
    [JsonPropertyName("lon")]
    public double Lon { get; set; }
}