using FluentValidation.TestHelper;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Application.Validators;
using WeatherForecasting.Domain.Enums;

namespace WeatherForecasting.Tests.ApplicationTests.ValidatorsTests;

public class GetWeatherForecastForFiveDaysByLonAndLatQueryValidatorTests
{
    private readonly GetWeatherForecastForFiveDaysByLonAndLatQueryValidator _validator = new();

    [Fact]
    public void Should_Pass_Validation_When_All_Fields_Are_Valid()
    {
        var query = new GetWeatherForecastForFiveDaysByLonAndLatQuery(
            Latitude: 40.7128,
            Longitude: -74.0060,
            Provider: WeatherProviderType.OpenWeather
        );

        var result = _validator.TestValidate(query);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(-91)]
    [InlineData(91)]
    public void Should_Have_Error_When_Latitude_Out_Of_Range(double latitude)
    {
        var query = new GetWeatherForecastForFiveDaysByLonAndLatQuery(
            Latitude: latitude,
            Longitude: 0,
            Provider: WeatherProviderType.OpenWeather
        );

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.Latitude)
              .WithErrorMessage("Latitude must be between -90 and 90 degrees.");
    }

    [Theory]
    [InlineData(-181)]
    [InlineData(181)]
    public void Should_Have_Error_When_Longitude_Out_Of_Range(double longitude)
    {
        var query = new GetWeatherForecastForFiveDaysByLonAndLatQuery(
            Latitude: 0,
            Longitude: longitude,
            Provider: WeatherProviderType.OpenWeather
        );

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.Longitude)
              .WithErrorMessage("Longitude must be between -180 and 180 degrees.");
    }

    [Fact]
    public void Should_Have_Error_When_Provider_Is_Unknown()
    {
        var query = new GetWeatherForecastForFiveDaysByLonAndLatQuery(
            Latitude: 0,
            Longitude: 0,
            Provider: WeatherProviderType.Unknown
        );

        var result = _validator.TestValidate(query);

        result.ShouldHaveValidationErrorFor(x => x.Provider)
              .WithErrorMessage("Provider must be provided.");
    }
}