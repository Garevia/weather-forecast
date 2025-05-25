using System.Text.Json;
using Microsoft.Extensions.Options;
using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.DTO;
using WeatherForecasting.Infrastructure.Utilities;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;

public class OpenWeatherMapServiceClient : IWeatherServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<OpenWeatherMapServiceClient> _logger;
    private readonly string _apiKey;

    public OpenWeatherMapServiceClient(HttpClient httpClient, 
        ILogger<OpenWeatherMapServiceClient> logger, 
        IOptions<OpenMapWeatherApiOptions> options)
    {
        _logger = logger;
        _apiKey = options.Value.ApiKey
                  ?? throw new ArgumentNullException("OpenWeatherMap API key is not configured");
        _httpClient = httpClient;
    }

    public async Task<Result<WeatherDto>> GetWeatherForecastByCityAsync(string city, string country)
    {
        try
        {
            var url = string.Format(OpenWeatherApiEndpoints.CurrentWeatherByCity, city, country, _apiKey);

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return Result<WeatherDto>.Failure($"Error in getting data, error code {response.StatusCode}", response.StatusCode);
            }
            var content = await response.Content.ReadAsStringAsync();

            var forecast = JsonSerializer.Deserialize<OpenWeatherResponse>(content);

            var weather = new WeatherDto()
            {
                City = forecast.name,
                CountryCode = forecast.System.Country,
                Description = forecast.weather[0].Description,
                TemperatureCelsius = (decimal)forecast.main.Temperature,
                Date = TimeHelper.FromUnixTimeSeconds(forecast.dt)
            };
            
            return Result<WeatherDto>.Success(weather);
        }
        catch (Exception ex)
        {
            return Result<WeatherDto>.Failure("Unexpected error occurred: " + ex.Message);
        }
    }

    public async Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByCityAsync(string city, string countryCode)
    {
        try
        {
            var url = string.Format(OpenWeatherApiEndpoints.Forecast5DayByCity, city, countryCode,
                _apiKey);

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return Result<WeatherForFiveDaysDto>.Failure($"Error in getting data, error code {response.StatusCode}", response.StatusCode);
            }
            var content = await response.Content.ReadAsStringAsync();
            
            var forecast = JsonSerializer.Deserialize<OpenWeatherForecastResponse>(content);

            var weather = new WeatherForFiveDaysDto()
            {
                City = city,
                CountryCode = countryCode,
                Forecasts = forecast.list.Select(x => new WeatherDto()
                {
                    City = forecast.City.Name,
                    CountryCode = forecast.City.Country,
                    Description = x.Weather[0].Description,
                    TemperatureCelsius = (decimal)x.Main.Temperature,
                    Date = TimeHelper.FromUnixTimeSeconds(x.Timestamp)
                }).ToList(),
            };
            
            return Result<WeatherForFiveDaysDto>.Success(weather);
        }
        catch (Exception ex)
        {
            return Result<WeatherForFiveDaysDto>.Failure("Unexpected error occurred: " + ex.Message);
        }
    }
    
    public async Task<Result<WeatherDto>> GetWeatherForecastByLonAndLanAsync(double longitude, double latitude)
    {
        try
        {
            var url = string.Format(OpenWeatherApiEndpoints.CurrentWeatherByLongitudeAndLatitude, latitude, longitude,
                _apiKey);

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return Result<WeatherDto>.Failure($"Error in getting data, error code {response.StatusCode}", response.StatusCode);
            }
            var content = await response.Content.ReadAsStringAsync();
            
            var forecast = JsonSerializer.Deserialize<OpenWeatherResponse>(content);
            var weather = new WeatherDto()
            {
                City = forecast.name,
                CountryCode = forecast.System.Country,
                Description = forecast.weather[0].Description,
                TemperatureCelsius = (decimal)forecast.main.Temperature,
                Date = TimeHelper.FromUnixTimeSeconds(forecast.dt)
            };
            
            return Result<WeatherDto>.Success(weather);
        }
        catch (Exception ex)
        {
            return Result<WeatherDto>.Failure("Unexpected error occurred: " + ex.Message);
        }
    }

    public async Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByLonAndLatAsync(double longitude, double latitude)
    {
        try
        {
            var url = string.Format(OpenWeatherApiEndpoints.Forecast5Day, latitude, longitude,
                _apiKey);

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return Result<WeatherForFiveDaysDto>.Failure($"Error in getting data, error code {response.StatusCode}", response.StatusCode);
            }
            var content = await response.Content.ReadAsStringAsync();
            
            var forecast = JsonSerializer.Deserialize<OpenWeatherForecastResponse>(content);

            var weather = new WeatherForFiveDaysDto()
            {
                City = forecast.City.Name,
                CountryCode = forecast.City.Country,
                Forecasts = forecast.list.Select(x => new WeatherDto()
                {
                    City = forecast.City.Country,
                    CountryCode = forecast.City.Country,
                    Description = x.Weather[0].Description,
                    TemperatureCelsius = (decimal)x.Main.Temperature,
                    Date = TimeHelper.FromUnixTimeSeconds(x.Timestamp)
                }).ToList(),
            };
            
            return Result<WeatherForFiveDaysDto>.Success(weather);
        }
        catch (Exception ex)
        {
            return Result<WeatherForFiveDaysDto>.Failure("Unexpected error occurred: " + ex.Message);
        }
    }
}
