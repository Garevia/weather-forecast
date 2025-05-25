using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Queries;

/// <summary>
/// Represents a query to retrieve the weather forecast based on geographic coordinates 
/// (longitude and latitude) using the specified weather provider.
/// </summary>
/// <param name="Longitude">The longitude of the location.</param>
/// <param name="Latitude">The latitude of the location.</param>
/// <param name="Provider">The weather provider to use for fetching the forecast data.</param>
public record GetWeatherForecastByLonAndLatQuery(double Longitude, double Latitude, WeatherProviderType Provider) : IRequest<Result<WeatherForecastDto>>;