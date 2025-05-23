using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Domain.Entities;

namespace WeatherForecasting.Application.Mappers;

public static class OpenWeatherMapper
{
    public static WeatherForecastDto ToDomain(WeatherForecast response)
    {
        return new WeatherForecastDto()
        {
            DateTime = response.DateTime,
            TemperatureCelsius = response.TemperatureCelsius,
            WeatherProvider = response.WeatherProvider,
            Description = response.Description
        };
    }

    public static WeatherForecastForFiveDaysDto ToDomain(WeatherForecastForFiveDays response)
    {
        return new WeatherForecastForFiveDaysDto()
        {
           City = response.City,
           CountryCode = response.CountryCode,
           WeatherForecasts = response.Forecasts.Select(x => new WeatherForecastForTimeStampDto()
           {
               DateTime = x.DateTime,
               TemperatureCelsius = x.TemperatureCelsius,
               Description = x.Description,
               Provider = x.WeatherProvider,
           }).ToList()
        };
    }
    
    public static GeolocationDto ToDomain(Geolocation response)
    {
        return new GeolocationDto()
        {
           Longittude = response.Longittude,
           Lattitude = response.Lattitude
        };
    }
}