using MediatR;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Queries;

public record GetWeatherForecastForFiveDaysQuery(double lon, double lat, WeatherProvider Provider) : IRequest<WeatherForecastForFiveDaysDto>;