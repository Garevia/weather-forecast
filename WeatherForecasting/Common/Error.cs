using System.Net;

namespace WeatherForecasting.Common;

/// <summary>
/// Represents an error with a descriptive message and an optional HTTP status code.
/// </summary>
public class Error
{
    public string Message { get; }
    public HttpStatusCode? HttpStatusCode { get; }

    public Error(string message, HttpStatusCode? httpStatusCode = null)
    {
        Message = message;
        HttpStatusCode = httpStatusCode;
    }
}
