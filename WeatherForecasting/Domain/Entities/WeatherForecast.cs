using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Domain.Entities
{
    public class WeatherForecast
    {
        public string City { get; }
        public string CountryCode { get; set; }
        public string Description { get; }
        public decimal TemperatureCelsius { get; }
        public DateTimeOffset DateTime { get; set; }
        public WeatherProviderType WeatherProvider { get; set; }
        
        public WeatherForecast(string city, string countryCode, string description, decimal temperatureCelsius, DateTimeOffset dateTime)
        {
            City = city;
            Description = description;
            TemperatureCelsius = temperatureCelsius;
            CountryCode = countryCode;
            DateTime = dateTime;
        }
    }
}

