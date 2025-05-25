using System.Globalization;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Options;
using WeatherForecasting.Common;
using WeatherForecasting.Infrastructure.DTO;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Mapper;
using WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Models;

namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient;

public class WeatherstackServiceClient : IWeatherServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherstackServiceClient> _logger;
    private readonly string _apiKey;

    public WeatherstackServiceClient(HttpClient httpClient, 
        ILogger<WeatherstackServiceClient> logger, 
        IOptions<WeatherstackWeatherApiOptions> options)
    {
        _logger = logger;
        _apiKey = options.Value.ApiKey
                  ?? throw new ArgumentNullException("Weatherstack API key is not configured");
        _httpClient = httpClient;
    }

    public async Task<Result<WeatherDto>> GetWeatherForecastByCityAsync(string city, string country)
    {
        try
        {
            var url = string.Format(WeatherstackWeatherApiEndpoints.CurrentWeather, _apiKey, city, country);

            var response = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorModel = JsonSerializer.Deserialize<ClientError>(content);

                return Result<WeatherDto>.Failure($"Error in getting data, error code {errorModel.Error.Code}", 
                    ErrorCodeMapper.MapToHttpStatusCode(errorModel.Error.Code));
            }

            var forecast = JsonSerializer.Deserialize<WeatherstackCurrentResponse>(content);

            var weather = new WeatherDto()
            {
                City = forecast.Location.Name,
                CountryCode =  forecast.Location.Country,
                Description = string.Join(",", forecast.Current.WeatherDescriptions),
                TemperatureCelsius = (decimal)forecast.Current.Temperature,
                Date = DateTime.ParseExact(forecast.Location.LocalTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            };
            
            return Result<WeatherDto>.Success(weather);
        }
        catch (Exception ex)
        {
            return Result<WeatherDto>.Failure("Unexpected error occurred: " + ex.Message);
        }
    }

    public async Task<Result<WeatherDto>> GetWeatherForecastByLonAndLanAsync(double lon, double lat)
    {
        try
        {
            var url = string.Format(WeatherstackWeatherApiEndpoints.CurrentWeather, _apiKey, lat, lon);

            var response = await _httpClient.GetAsync(url);

            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                var errorModel = JsonSerializer.Deserialize<ClientError>(content);

                return Result<WeatherDto>.Failure($"Error in getting data, error code {errorModel.Error.Code}", 
                    ErrorCodeMapper.MapToHttpStatusCode(errorModel.Error.Code));
            }

            var forecast = JsonSerializer.Deserialize<WeatherstackCurrentResponse>(content);

            var weather = new WeatherDto()
            {
                City = forecast.Location.Name,
                CountryCode =  forecast.Location.Country,
                Description = string.Join(",", forecast.Current.WeatherDescriptions),
                TemperatureCelsius = forecast.Current.Temperature,
                Date = DateTime.ParseExact(forecast.Location.LocalTime, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture)
            };
            
            return Result<WeatherDto>.Success(weather);
        }
        catch (Exception ex)
        {
            return Result<WeatherDto>.Failure("Unexpected error occurred: " + ex.Message);
        }
    }

    public Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByLonAndLatAsync(double lon, double lat)
    {
        return Task.FromResult(Result<WeatherForFiveDaysDto>.Failure("Method not supported for provider WeatherStack",
            HttpStatusCode.Forbidden));
    }

    public Task<Result<WeatherForFiveDaysDto>> GetFiveDayForecastByCityAsync(string city, string countryCode)
    {
        return Task.FromResult(Result<WeatherForFiveDaysDto>.Failure("Method not supported for provider WeatherStack",
            HttpStatusCode.Forbidden));
    }
}