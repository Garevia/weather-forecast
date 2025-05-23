namespace WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;

public class OpenWeatherForecastResponse
{
    public int Message { get; set; }
    public List<List> list { get; set; }
    public City City { get; set; }
}

public class List
{
    public int dt { get; set; }
    public Main Main { get; set; }
    public List<Weather> Weather { get; set; }
}

public class City
{
    public string Name { get; set; }
    public string Country { get; set; }
}