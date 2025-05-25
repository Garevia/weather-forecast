using FluentValidation;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Validators;

public class GetWeatherForecastByCityQueryValidator :  AbstractValidator<GetWeatherForecastByCityQuery>
{
    public GetWeatherForecastByCityQueryValidator()
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