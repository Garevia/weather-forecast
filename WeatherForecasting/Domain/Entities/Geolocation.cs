namespace WeatherForecasting.Domain.Entities;

/// <summary>
/// Represents a geographic location defined by latitude and longitude coordinates.
/// </summary>
public class Geolocation
{
    public Geolocation(double latitude, double longitude)
    {
        this.Latitude = latitude;
        this.Longitude = longitude;
    }
    
    public double Latitude { get; set; }

    public double Longitude { get; set; }
}