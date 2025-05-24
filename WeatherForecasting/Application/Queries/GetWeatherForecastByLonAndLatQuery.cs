using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Queries;

public record GetWeatherForecastByLonAndLatQuery(double Longitude, double Latitude, WeatherProviderType Provider) : IRequest<Result<WeatherForecastDto>>;