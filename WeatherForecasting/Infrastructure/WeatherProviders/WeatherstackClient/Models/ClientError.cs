namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Models;

/// <summary>
/// Represents a client response indicating an error occurred.
/// </summary>
public class ClientError
{
    public bool Success { get; set; }
    public Error Error { get; set; }
}

/// <summary>
/// Provides detailed information about an error.
/// </summary>
public class Error
{
    public ErrorCode Code { get; set; }
    public string Type { get; set; }
    public string Info { get; set; }
}

/// <summary>
/// Enumeration of possible error codes used in the application.
/// </summary>
public enum ErrorCode 
{
    NotFound = 404,
}