using FluentValidation;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Validators;

/// <summary>
/// Validator for <see cref="GetWeatherForecastForFiveDaysByCityQuery"/> ensuring that
/// the city, country code, and weather provider are valid and meet required constraints.
/// </summary>
/// <remarks>
/// - <c>City</c> must be provided and have a maximum length of 100 characters.
/// - <c>CountryCode</c> must be provided and have a maximum length of 3 characters.
/// - <c>Provider</c> must be a valid enum value and cannot be <see cref="WeatherProviderType.Unknown"/>.
/// </remarks>
public class GetWeatherForecastForFiveDaysByCityQueryValidator :  AbstractValidator<GetWeatherForecastForFiveDaysByCityQuery>
{ 
    public GetWeatherForecastForFiveDaysByCityQueryValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City must be provided.")
            .MaximumLength(100);
        
        RuleFor(x => x.CountryCode)
            .NotEmpty().WithMessage("Country must be provided.")
            .MaximumLength(3);

        RuleFor(x => x.Provider)
            .IsInEnum()
            .NotEqual(WeatherProviderType.Unknown)
            .WithMessage("Provider must be provided.");
    }
}