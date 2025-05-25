using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Queries;

/// <summary>
/// Represents a query to retrieve a 5-day weather forecast for a specific city and country,
/// using the specified weather provider.
/// </summary>
/// <param name="City">The name of the city for which the forecast is requested.</param>
/// <param name="CountryCode">The country code.</param>
/// <param name="Provider">The weather provider to use for fetching the 5-day forecast data.</param>
public record GetWeatherForecastForFiveDaysByCityQuery(string City, string CountryCode, WeatherProviderType Provider) : IRequest<Result<WeatherForecastForFiveDaysDto>>;
