using FluentValidation;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Application.Validators;

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