using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Queries;

public record GetWeatherForecastByCityQuery(string City, string CountryCode, WeatherProviderType Provider) : IRequest<Result<WeatherForecastDto>>;
