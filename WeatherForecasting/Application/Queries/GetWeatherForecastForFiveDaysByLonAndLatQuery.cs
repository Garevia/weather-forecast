using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Queries;

/// <summary>
/// Represents a query to retrieve a 5-day weather forecast based on geographic coordinates 
/// (latitude and longitude) using the specified weather provider.
/// </summary>
/// <param name="Latitude">The latitude of the location.</param>
/// <param name="Longitude">The longitude of the location.</param>
/// <param name="Provider">The weather provider to use for fetching the 5-day forecast data.</param>
public record GetWeatherForecastForFiveDaysByLonAndLatQuery(double Latitude, double Longitude, WeatherProviderType Provider) : IRequest<Result<WeatherForecastForFiveDaysDto>>;