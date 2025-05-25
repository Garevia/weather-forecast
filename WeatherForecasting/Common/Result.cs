using System.Net;
using System.Text.Json.Serialization;

namespace WeatherForecasting.Common;

/// <summary>
/// Result data for getting data and error
/// </summary>
/// <typeparam name="T"></typeparam>
public class Result<T>
{
    public T? Value { get; }
    public Error? Error { get; }
    public bool IsSuccess => Error == null;
    
    [JsonConstructor]
    protected Result(T? value, Error? error = null)
    {
        Value = value;
        Error = error;
    }
    
    public static Result<T> Success(T value) => new(value);

    public static Result<T> Failure(string message, HttpStatusCode? statusCode = null) =>
        new(default, new Error(message, statusCode));
}