using System.Text.Json;
using Microsoft.Extensions.Options;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Utilities;
using WeatherForecasting.Infrastructure.WeatherProviders.Models;

namespace WeatherForecasting.Infrastructure.WeatherProviders;

public class OpenWeatherMapService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OpenWeatherMapService> _logger;
    private readonly string _apiKey;
    
    public OpenWeatherMapService(HttpClient httpClient, 
        ILogger<OpenWeatherMapService> logger, 
        IOptions<WeatherApiOptions> options)
    {
        _logger = logger;
        _apiKey = options.Value.ApiKey
                  ?? throw new ArgumentNullException("OpenWeatherMap API key is not configured");
        _httpClient = httpClient;
    }

    public async Task<WeatherForecast> GetWeatherForecastByCityAsync(string city, string country)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City name is required");
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country code is required");
            
            var url = string.Format(WeatherApiEndpoints.CurrentWeatherByCity, city, country, _apiKey);

            var response = await _httpClient.GetStringAsync(url);
            
            var forecast = JsonSerializer.Deserialize<OpenWeatherResponse>(response);
            
            return new WeatherForecast(
                forecast.name,
                forecast.weather[0].description,  
                (decimal)forecast.main.temp, 
                TimeHelper.FromUnixTimeSeconds(forecast.dt) );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching weather data for city {City}", city);
            throw;
        }
    }

    public async Task<WeatherForecast> GetWeatherForecastByLonAndLanAsync(double lon, double lat)
    {
        try
        {
            var url = string.Format(WeatherApiEndpoints.CurrentWeatherByLongitudeAndLattitude, lat, lon,
                _apiKey);

            var response = await _httpClient.GetStringAsync(url);

            var forecast = JsonSerializer.Deserialize<OpenWeatherResponse>(response);
            return new WeatherForecast(
                forecast.name, 
                forecast.weather[0].description,  
                (decimal)forecast.main.temp, 
                TimeHelper.FromUnixTimeSeconds(forecast.dt));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching weather data for long {long} and  lat {lat}", lon, lat);
            throw;
        }    
    }

    public async Task<WeatherForecastForFiveDays> GetFiveDayForecastAsync(double lon, double lat)
    {
        try
        {
            var url = string.Format(WeatherApiEndpoints.Forecast5Day, lat, lon,
                _apiKey);

            var response = await _httpClient.GetStringAsync(url);

            var forecast = JsonSerializer.Deserialize<OpenWeatherForecastResponse>(response);

            var result = new WeatherForecastForFiveDays()
            {
                WeatherForecasts = forecast.list.Select(x => new WeatherForecastForTimeStamp()
                {
                    Description = x.weather[0].description,
                    TemperatureCelsius = (decimal)x.main.temp,
                    DateTime = TimeHelper.FromUnixTimeSeconds(x.dt)
                }).ToList(),
            };
            
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching weather data for long {long} and  lat {lat}", lon, lat);
            throw;
        }   
    }
}
