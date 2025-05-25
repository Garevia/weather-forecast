using System.Text.Json.Serialization;

namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

/// <summary>
/// Represents the main weather data response from the OpenWeather API.
/// </summary>
public class OpenWeatherResponse
{ 
    public List<Weather> weather { get; set; } 
    public int dt { get; set; } 
    public string name { get; set; } 
    public Main main { get; set; }
    
    [JsonPropertyName("sys")] 
    public System System { get; set; }
}

/// <summary>
/// Represents weather condition information.
/// </summary>
public class Weather
{
    [JsonPropertyName("description")] 
    public string Description { get; set; } 
}

/// <summary>
/// Represents system information including country code.
/// </summary>
public class System 
{
        [JsonPropertyName("country")]
        public string Country { get; set; } 
}

/// <summary>
/// Represents main weather metrics such as temperature.
/// </summary>
public class Main
{
    [JsonPropertyName("temp")] public float Temperature { get; set; }
}

