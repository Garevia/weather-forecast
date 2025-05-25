using FluentValidation.TestHelper;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Application.Validators;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Tests.ApplicationTests.ValidatorsTests;

public class GetGeocodingQueryValidatorTests
{
    private readonly GetGeocodingQueryValidator _validator;

    public GetGeocodingQueryValidatorTests()
    {
        _validator = new GetGeocodingQueryValidator();
    }

    [Fact]
    public void Should_Have_Error_When_City_Is_Empty()
    {
        var query = new GetGeocodingQuery("", "US", WeatherProviderType.Weatherstack);
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.City);
    }

    [Fact]
    public void Should_Have_Error_When_CountryCode_Is_Empty()
    {
        var query = new GetGeocodingQuery("New York", "", WeatherProviderType.Weatherstack);

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.CountryCode);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Query_Is_Valid()
    {
        var query = new GetGeocodingQuery("Berlin", "DE", WeatherProviderType.OpenWeather);

        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_City_Too_Long()
    {
        string longCityName = new string('A', 111);

        var query = new GetGeocodingQuery(longCityName, "US", WeatherProviderType.OpenWeather);
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.City);
    }

    [Fact]
    public void Should_Have_Error_When_CountryCode_Too_Long()
    {
        var query = new GetGeocodingQuery("Boston", "US33", WeatherProviderType.OpenWeather);

        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.CountryCode);
    }
    
    [Fact]
    public void Should_Have_Error_When_Provider_Is_Unknown()
    {
        // Only valid if WeatherProviderType is nullable (WeatherProviderType?)
        var query = new GetGeocodingQuery("Boston", "US33", WeatherProviderType.Unknown);


        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.Provider)
            .WithErrorMessage("Provider must be provided.");
    }
}