using FluentValidation;
using WeatherForecasting.Application.Queries;

namespace WeatherForecasting.Application.Validators;

public class GetGeocodingQueryValidator :  AbstractValidator<GetGeocodingQuery>
{
    public GetGeocodingQueryValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City must be provided.")
            .MaximumLength(100);
        
        RuleFor(x => x.CountryCode)
            .NotEmpty().WithMessage("Country must be provided.")
            .MaximumLength(3);

        RuleFor(x => x.Provider)
            .NotNull().WithMessage("Provider must be provided.");
    }
}