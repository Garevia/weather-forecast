namespace WeatherForecasting.Infrastructure.DTO;

/// <summary>
/// Data Transfer Object representing geographic coordinates,
/// used to transfer geolocation data between layers or systems.
/// </summary>
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