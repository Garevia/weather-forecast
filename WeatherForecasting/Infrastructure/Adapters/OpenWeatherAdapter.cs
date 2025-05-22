using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Domain.Entities;

namespace WeatherForecasting.Infrastructure.Adapters;

public class OpenWeatherAdapter
{
    public static WeatherForecastDto ToDomain(WeatherForecast response)
    {
        return new WeatherForecastDto()
        {
            TemperatureCelsius = response.TemperatureCelsius,
            Description = response.Description,
            City = response.City,
            DateTime = response.DateTime
        };
    }

    public static WeatherForecastForFiveDaysDto ToDomain(WeatherForecastForFiveDays response)
    {
        return new WeatherForecastForFiveDaysDto()
        {
           WeatherForecasts = response.WeatherForecasts.Select(x => new WeatherForecastForTimeStampDto()
           {
               DateTime = x.DateTime,
               TemperatureCelsius = x.TemperatureCelsius,
               Description = x.Description,
           }).ToList(),
           City = response.City,
           CountryCode = response.CountryCode
        };
    }
}