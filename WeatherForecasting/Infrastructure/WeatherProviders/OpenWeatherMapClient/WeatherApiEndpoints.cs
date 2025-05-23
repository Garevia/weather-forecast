namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;

public static class WeatherApiEndpoints
{
    public const string CurrentWeatherByCity = "/data/2.5/weather?q={0},{1}&units=metric&appid={2}";

    public const string CurrentWeatherByLongitudeAndLattitude =
        "/data/2.5/weather?lat={0}&lon={1}&units=metric&appid={2}";
    
    public const string Forecast5Day = "https://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid={2}";
    public const string GeocodingDirect = "geo/1.0/direct?q={0},{1}&limit=1&appid={2}";
}