using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Domain.Entities
{
    public class WeatherForecast
    {
        public string City { get; }
        public string Description { get; }
        public decimal TemperatureCelsius { get; }
        public DateTimeOffset DateTime { get; set; }
        
        public string CountryCode { get; set; }
        public WeatherProvider WeatherProvider { get; set; }
        public WeatherForecast(string city, string description, decimal temperatureCelsius, DateTimeOffset dateTime)
        {
            City = city;
            Description = description;
            TemperatureCelsius = temperatureCelsius;
            DateTime = dateTime;
        }
    }
}

