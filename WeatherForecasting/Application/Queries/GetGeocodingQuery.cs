using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Queries;

/// <summary>
/// Represents a query to retrieve geolocation data (latitude and longitude) 
/// based on the specified city and country code using the given weather provider.
/// </summary>
/// <param name="City">The name of the city to geocode.</param>
/// <param name="CountryCode">country code.</param>
/// <param name="Provider">The weather provider to use for the geocoding service.</param>
public record GetGeocodingQuery(string City, string CountryCode, WeatherProviderType Provider) : IRequest<Result<GeolocationDto>>;
