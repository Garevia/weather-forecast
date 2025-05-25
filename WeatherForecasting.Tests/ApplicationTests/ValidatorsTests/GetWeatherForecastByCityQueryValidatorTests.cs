using FluentValidation.TestHelper;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Application.Validators;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Tests.ApplicationTests.ValidatorsTests;

public class GetWeatherForecastByCityQueryValidatorTests
{
  
    private readonly GetWeatherForecastByCityQueryValidator _validator = new();

    [Fact]
    public void Should_Pass_Validation_When_All_Fields_Are_Valid()
    {
        var query = new GetWeatherForecastByCityQuery(
            City: "Yerevan",
            CountryCode: "AM",
            Provider: WeatherProviderType.OpenWeather
        );

        var result = _validator.TestValidate(query);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_City_Is_Empty()
    {
        var query = new GetWeatherForecastByCityQuery(
            City: "",
            CountryCode: "AM",
            Provider: WeatherProviderType.OpenWeather
        );

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.City)
              .WithErrorMessage("City must be provided.");
    }

    [Fact]
    public void Should_Have_Error_When_City_Exceeds_Max_Length()
    {
        var query = new GetWeatherForecastByCityQuery(
            City: new string('a', 101),
            CountryCode: "AM",
            Provider: WeatherProviderType.OpenWeather
        );

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.City);
    }

    [Fact]
    public void Should_Have_Error_When_CountryCode_Is_Empty()
    {
        var query = new GetWeatherForecastByCityQuery(
            City: "Yerevan",
            CountryCode: "",
            Provider: WeatherProviderType.OpenWeather
        );

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.CountryCode)
              .WithErrorMessage("Country must be provided.");
    }

    [Fact]
    public void Should_Have_Error_When_CountryCode_Exceeds_Max_Length()
    {
        var query = new GetWeatherForecastByCityQuery(
            City: "Yerevan",
            CountryCode: "ARMN",
            Provider: WeatherProviderType.OpenWeather
        );

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.CountryCode);
    }

    [Fact]
    public void Should_Have_Error_When_Provider_Is_Unknown()
    {
        // Only valid if WeatherProviderType is nullable (WeatherProviderType?)
        var query = new GetWeatherForecastByCityQuery(
            City: "Yerevan",
            CountryCode: "AM",
            Provider: WeatherProviderType.Unknown
        );

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.Provider)
              .WithErrorMessage("Provider must be provided.");
    }
}
