using MediatR;
using WeatherForecasting.Application.Weather.DTO;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Queries;

public record GetWeatherForecastForFiveDaysByLonAndLatQuery(double Latitude, double Longitude, WeatherProvider Provider) : IRequest<WeatherForecastForFiveDaysDto>;