using MediatR;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Queries;

public record GetWeatherForecastForFiveDaysByCityQuery(string City, string CountryCode, WeatherProvider Provider) : IRequest<WeatherForecastForFiveDaysDto>;
