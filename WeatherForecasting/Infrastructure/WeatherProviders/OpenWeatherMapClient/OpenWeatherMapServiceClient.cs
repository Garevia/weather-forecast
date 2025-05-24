using System.Text.Json;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using WeatherForecasting.Domain.Entities;
using WeatherForecasting.Infrastructure.Common;
using WeatherForecasting.Infrastructure.Utilities;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;

public class OpenWeatherMapServiceClient : IWeatherServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OpenWeatherMapServiceClient> _logger;
    private readonly string _apiKey;
    private readonly TimeSpan _redisCacheDuration;
    private readonly IDatabase _redisDb;

    public OpenWeatherMapServiceClient(HttpClient httpClient, 
        ILogger<OpenWeatherMapServiceClient> logger, 
        IOptions<WeatherApiOptions> options,
        IOptions<RedisOptions> redisOptions,
        ConnectionMultiplexer redis)
    {
        _logger = logger;
        _apiKey = options.Value.ApiKey
                  ?? throw new ArgumentNullException("OpenWeatherMap API key is not configured");
        _httpClient = httpClient;
        _redisDb = redis.GetDatabase();

    }

    public async Task<WeatherForecast> GetWeatherForecastByCityAsync(string city, string country)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("City name is required");
            if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException("Country code is required");
            
            string cacheKey = $"weather:{city}{country}";
            var cachedData = await _redisDb.StringGetAsync(cacheKey);
            if (cachedData.HasValue)
            {
                var cachedForecast = JsonSerializer.Deserialize<OpenWeatherResponse>(cachedData);
            
                return new WeatherForecast(
                    cachedForecast.name,
                    cachedForecast.weather[0].Description,  
                    (decimal)cachedForecast.main.Temperature, 
                    TimeHelper.FromUnixTimeSeconds(cachedForecast.dt));
            }
            
            var url = string.Format(WeatherApiEndpoints.CurrentWeatherByCity, city, country, _apiKey);

            var response = await _httpClient.GetStringAsync(url);
            
            var forecast = JsonSerializer.Deserialize<OpenWeatherResponse>(response);
            await _redisDb.StringSetAsync(cacheKey, response, _redisCacheDuration);

            return new WeatherForecast(
                forecast.name,
                forecast.weather[0].Description,  
                (decimal)forecast.main.Temperature, 
                TimeHelper.FromUnixTimeSeconds(forecast.dt));
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error fetching weather data for city {City}", city);
            throw new WeatherApiException("Failed to fetch weather data from OpenWeather.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while calling OpenWeather");
            throw new WeatherApiException("Unexpected error while calling OpenWeather.", ex);
        }
    }

    public async Task<WeatherForecastForFiveDays> GetFiveDayForecastAsync(string city, string countryCode)
    {
        try
        {
            var url = string.Format(WeatherApiEndpoints.Forecast5DayByCity, city, countryCode,
                _apiKey);

            var response = await _httpClient.GetStringAsync(url);

            var forecast = JsonSerializer.Deserialize<OpenWeatherForecastResponse>(response);

            var result = new WeatherForecastForFiveDays
            {
                CountryCode = countryCode ,
                City = city,
                Forecasts = forecast.list.Select(x => new WeatherForecast(forecast.City.Name, 
                    x.Weather[0].Description,  
                    (decimal)x.Main.Temperature, 
                    TimeHelper.FromUnixTimeSeconds(x.Timestamp))).ToList(),
            };
            
            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error fetching weather data for city {City}", city);
            throw new WeatherApiException("Failed to fetch weather data from OpenWeather.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while calling OpenWeather");
            throw new WeatherApiException("Unexpected error while calling OpenWeather.", ex);
        }
    }
    
    public async Task<WeatherForecast> GetWeatherForecastByLonAndLanAsync(double longitude, double latitude)
    {
        try
        {
            var url = string.Format(WeatherApiEndpoints.CurrentWeatherByLongitudeAndLattitude, latitude, longitude,
                _apiKey);

            var response = await _httpClient.GetStringAsync(url);

            var forecast = JsonSerializer.Deserialize<OpenWeatherResponse>(response);
            return new WeatherForecast(
                forecast.name, 
                forecast.weather[0].Description,  
                (decimal)forecast.main.Temperature, 
                TimeHelper.FromUnixTimeSeconds(forecast.dt));
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error fetching weather data for latitude {Latitude}, {Longitude}", latitude, longitude);
            throw new WeatherApiException("Failed to fetch weather data from OpenWeather.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while calling OpenWeather");
            throw new WeatherApiException("Unexpected error while calling OpenWeather.", ex);
        }
    }

    public async Task<WeatherForecastForFiveDays> GetFiveDayForecastAsync(double longitude, double latitude)
    {
        try
        {
            var url = string.Format(WeatherApiEndpoints.Forecast5Day, latitude, longitude,
                _apiKey);

            var response = await _httpClient.GetStringAsync(url);

            var forecast = JsonSerializer.Deserialize<OpenWeatherForecastResponse>(response);

            var result = new WeatherForecastForFiveDays
            {
                CountryCode = forecast.City.Country,
                City = forecast.City.Name,
                Forecasts = forecast.list.Select(x => new WeatherForecast(forecast.City.Name, 
                    x.Weather[0].Description,  
                    (decimal)x.Main.Temperature, 
                    TimeHelper.FromUnixTimeSeconds(x.Timestamp))).ToList(),
            };
            
            return result;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error fetching weather data for latitude {Latitude}, {Longitude}", latitude, longitude);
            throw new WeatherApiException("Failed to fetch weather data from OpenWeather.", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while calling OpenWeather");
            throw new WeatherApiException("Unexpected error while calling OpenWeather.", ex);
        }
    }
}
