using FluentValidation;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Validators;

/// <summary>
/// Validator for <see cref="GetWeatherForecastByLonAndLatQuery"/> ensuring that
/// the latitude, longitude, and weather provider are valid and within expected ranges.
/// </summary>
/// <remarks>
/// - <c>Latitude</c> must be between -90 and 90 degrees.
/// - <c>Longitude</c> must be between -180 and 180 degrees.
/// - <c>Provider</c> must be a valid enum value and cannot be <see cref="WeatherProviderType.Unknown"/>.
/// </remarks>
public class GetWeatherForecastByLonAndLatQueryValidator :  AbstractValidator<GetWeatherForecastByLonAndLatQuery>
{
    public GetWeatherForecastByLonAndLatQueryValidator()
    {
        RuleFor(x => x.Latitude)
            .InclusiveBetween(-90, 90)
            .WithMessage("Latitude must be between -90 and 90 degrees.");

        RuleFor(x => x.Longitude)
            .InclusiveBetween(-180, 180)
            .WithMessage("Longitude must be between -180 and 180 degrees.");
        
        RuleFor(x => x.Provider)
            .IsInEnum()
            .NotEqual(WeatherProviderType.Unknown)
            .WithMessage("Provider must be provided.");
    }
}