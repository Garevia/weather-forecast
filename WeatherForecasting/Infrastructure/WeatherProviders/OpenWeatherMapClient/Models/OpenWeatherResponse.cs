using System.Text.Json.Serialization;

namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models
{
    public class OpenWeatherResponse
    {
       public List<Weather> weather { get; set; }
       public int dt { get; set; }
       public string name { get; set; }
       public Main main { get; set; }
       
       [JsonPropertyName("sys")]
       public System System { get; set; }
    }

    public class Weather
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class System
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }   
    }
    
    public class Main
    {
        [JsonPropertyName("temp")]
        public float Temperature { get; set; }
    }
}
