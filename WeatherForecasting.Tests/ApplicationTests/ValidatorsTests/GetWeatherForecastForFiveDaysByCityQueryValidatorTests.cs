using FluentValidation.TestHelper;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Application.Validators;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Tests.ApplicationTests.ValidatorsTests;

public class GetWeatherForecastForFiveDaysByCityQueryValidatorTests
{
    private readonly GetWeatherForecastForFiveDaysByCityQueryValidator _validatorTests = new();

    [Fact]
    public void Should_Pass_Validation_When_All_Fields_Are_Valid()
    {
        var query = new GetWeatherForecastForFiveDaysByCityQuery(
            City: "New York",
            CountryCode: "US",
            Provider: WeatherProviderType.OpenWeather
        );

        var result = _validatorTests.TestValidate(query);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_City_Is_Empty()
    {
        var query = new GetWeatherForecastForFiveDaysByCityQuery(
            City: "",
            CountryCode: "US",
            Provider: WeatherProviderType.OpenWeather
        );

        var result = _validatorTests.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.City)
              .WithErrorMessage("City must be provided.");
    }

    [Fact]
    public void Should_Have_Error_When_City_Exceeds_Max_Length()
    {
        var city = new string('a', 101);
        var query = new GetWeatherForecastForFiveDaysByCityQuery(
            City: city,
            CountryCode: "US",
            Provider: WeatherProviderType.OpenWeather
        );

        var result = _validatorTests.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.City);
    }

    [Fact]
    public void Should_Have_Error_When_CountryCode_Is_Empty()
    {
        var query = new GetWeatherForecastForFiveDaysByCityQuery(
            City: "Paris",
            CountryCode: "",
            Provider: WeatherProviderType.OpenWeather
        );

        var result = _validatorTests.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.CountryCode)
              .WithErrorMessage("Country must be provided.");
    }

    [Fact]
    public void Should_Have_Error_When_CountryCode_Exceeds_Max_Length()
    {
        var countryCode = new string('a', 4);
        var query = new GetWeatherForecastForFiveDaysByCityQuery(
            City: "Paris",
            CountryCode: countryCode,
            Provider: WeatherProviderType.OpenWeather
        );

        var result = _validatorTests.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.CountryCode);
    }

    [Fact]
    public void Should_Have_Error_When_Provider_Is_Unknown()
    {
        var query = new GetWeatherForecastForFiveDaysByCityQuery(
            City: "Paris",
            CountryCode: "FR",
            Provider: WeatherProviderType.Unknown
        );

        var result = _validatorTests.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.Provider)
              .WithErrorMessage("Provider must be provided.");
    }
}