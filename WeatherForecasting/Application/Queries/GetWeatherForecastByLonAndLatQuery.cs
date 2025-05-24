using MediatR;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Queries;

public record GetWeatherForecastByLonAndLatQuery(double Longitude, double Latitude, WeatherProvider Provider) : IRequest<WeatherForecastDto>;