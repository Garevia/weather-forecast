using System.Net;

namespace WeatherForecasting.Common;

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
