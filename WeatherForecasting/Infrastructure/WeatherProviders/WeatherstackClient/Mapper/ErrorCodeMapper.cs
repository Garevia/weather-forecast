using System.Net;
using WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Models;

namespace WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Mapper;

/// <summary>
/// Maps domain-specific error codes to corresponding HTTP status codes.
/// </summary>
public class ErrorCodeMapper
{
    public static HttpStatusCode MapToHttpStatusCode(ErrorCode code) => code switch
    {
        ErrorCode.NotFound => HttpStatusCode.NotFound,
        _ => HttpStatusCode.InternalServerError
    };
}