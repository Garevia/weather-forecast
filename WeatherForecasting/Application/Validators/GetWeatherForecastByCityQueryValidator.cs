using FluentValidation;
using WeatherForecasting.Application.Queries;

namespace WeatherForecasting.Application.Validators;

public class GetWeatherForecastByCityQueryValidator :  AbstractValidator<GetWeatherForecastByCityQuery>
{
    public GetWeatherForecastByCityQueryValidator()
    {
        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City must be provided.")
            .MaximumLength(100);
        
        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country must be provided.")
            .MaximumLength(3);

        RuleFor(x => x.Provider)
            .NotNull().WithMessage("Provider must be provided.");
    }
}