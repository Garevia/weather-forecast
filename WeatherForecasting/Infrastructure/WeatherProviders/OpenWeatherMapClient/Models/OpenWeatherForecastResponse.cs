using System.Text.Json.Serialization;

namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

/// <summary>
/// Represents the response from the OpenWeather API for a weather forecast.
/// </summary>
public class OpenWeatherForecastResponse
{
    [JsonPropertyName("message")]
    public int Message { get; set; }
    
    [JsonPropertyName("list")]
    public List<List> List { get; set; }
    
    [JsonPropertyName("city")]
    public City City { get; set; }
}

/// <summary>
/// Represents a single forecast data point in the OpenWeather API response.
/// </summary>
public class List
{
    [JsonPropertyName("dt")]
    public int Timestamp { get; set; }
    
    [JsonPropertyName("main")]
    public Main Main { get; set; }
    
    [JsonPropertyName("weather")]
    public List<Weather> Weather { get; set; }
}

/// <summary>
/// Represents city information in the OpenWeather API response.
/// </summary>
public class City
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("country")]
    public string Country { get; set; }
}