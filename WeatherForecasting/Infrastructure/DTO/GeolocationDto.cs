namespace WeatherForecasting.Infrastructure.DTO;

public class GeolocationDto
{
    public GeolocationDto(double latitude, double longitude)
    {
        this.Latitude = latitude;
        this.Longitude = longitude;
    }
    
    public double Latitude { get; set; }

    public double Longitude { get; set; }
}