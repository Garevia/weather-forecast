using System.Text.Json.Serialization;

namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Models;

/// <summary>
/// Represents the response for the current weather from the Weatherstack API.
/// </summary>
public class WeatherstackCurrentResponse
{
    [JsonPropertyName("location")] 
    public WeatherstackLocation Location { get; set; }

    [JsonPropertyName("current")] 
    public WeatherstackCurrent Current { get; set; }
}

/// <summary>
/// Represents location information returned by Weatherstack.
/// </summary>
public class WeatherstackLocation
{
    [JsonPropertyName("name")] 
    public string Name { get; set; }

    [JsonPropertyName("country")] 
    public string Country { get; set; }

    [JsonPropertyName("localtime")]
    public string LocalTime { get; set; }
}

/// <summary>
/// Represents the current weather conditions from Weatherstack.
/// </summary>
public class WeatherstackCurrent
{
    [JsonPropertyName("temperature")] public int Temperature { get; set; }

    [JsonPropertyName("weather_descriptions")]
    public List<string> WeatherDescriptions { get; set; }
}