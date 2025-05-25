namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Models;

public class ClientError
{
    public bool Success { get; set; }
    public Error Error { get; set; }
}

public class Error
{
    public ErrorCode Code { get; set; }
    public string Type { get; set; }
    public string Info { get; set; }
}

public enum ErrorCode 
{
    NotFound = 404,
}