using System.Text.Json.Serialization;

namespace WeatherForecasting.Infrastructure.WeatherProviders.Models;

public class OpenWeatherGeolocation
{
    [JsonPropertyName("lat")]
    public double Lat { get; set; }
    
    [JsonPropertyName("lon")]
    public double Lon { get; set; }
}