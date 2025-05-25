using System.Text.Json.Serialization;

namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Models;

public class WeatherstackCurrentResponse
{
    [JsonPropertyName("location")] public WeatherstackLocation Location { get; set; }

    [JsonPropertyName("current")] public WeatherstackCurrent Current { get; set; }
}

public class WeatherstackLocation
{
    [JsonPropertyName("name")] public string Name { get; set; }


    [JsonPropertyName("country")] public string Country { get; set; }

    [JsonPropertyName("localtime")] public DateTimeOffset LocalTime { get; set; }
}

public class WeatherstackCurrent
{
    [JsonPropertyName("temperature")] public int Temperature { get; set; }

    [JsonPropertyName("weather_descriptions")]
    public List<string> WeatherDescriptions { get; set; }
}