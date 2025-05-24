using MediatR;
using WeatherForecasting.Application.DTO;
using WeatherForecasting.Common;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Queries;

public record GetWeatherForecastForFiveDaysByLonAndLatQuery(double Latitude, double Longitude, WeatherProviderType Provider) : IRequest<Result<WeatherForecastForFiveDaysDto>>;