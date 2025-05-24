namespace WeatherForecasting.Domain.Exceptions;

public class InvalidLocationException : Exception
{
    public InvalidLocationException(string city, string country)
        : base($"Invalid location: {city}, {country}.") { }
    
    public InvalidLocationException(double longitude, double latitude)
        : base($"Invalid location: {longitude}, {latitude}.") { }
}