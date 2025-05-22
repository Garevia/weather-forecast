using MediatR;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Queries;

public record GetWeatherForecastByCityQuery(string City, string Country, WeatherProvider Provider) : IRequest<WeatherForecastDto>;
