using System.Text.Json.Serialization;

namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

public class OpenWeatherForecastResponse
{
    [JsonPropertyName("message")]
    public int Message { get; set; }
    public List<List> list { get; set; }
    
    [JsonPropertyName("city")]
    public City City { get; set; }
}

public class List
{
    [JsonPropertyName("dt")]
    public int Timestamp { get; set; }
    
    [JsonPropertyName("main")]
    public Main Main { get; set; }
    
    [JsonPropertyName("weather")]
    public List<Weather> Weather { get; set; }
}

public class City
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("country")]
    public string Country { get; set; }
}