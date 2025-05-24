using MediatR;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Queries;

public record GetGeocodingQuery(string City, string CountryCode, WeatherProvider Provider) : IRequest<GeolocationDto>;
