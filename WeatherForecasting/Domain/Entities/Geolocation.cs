namespace WeatherForecasting.Domain.Entities;

public class Geolocation
{
    public Geolocation(double lattitude, double longittude)
    {
        this.Lattitude = lattitude;
        this.Longittude = longittude;
    }
    
    public double Lattitude { get; set; }

    public double Longittude { get; set; }
}