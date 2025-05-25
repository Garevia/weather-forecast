using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Services;
using WeatherForecasting.Application.Validators;
using WeatherForecasting.Infrastructure.WeatherProviders;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;
using WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient;
using WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Models;

namespace WeatherForecasting.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWeatherServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Logging
        services.AddSingleton<ILoggerFactory, LoggerFactory>();

        // Configuration binding
        services.Configure<OpenMapWeatherApiOptions>(
            configuration.GetSection("OpenWeatherMap"));
        services.Configure<WeatherstackWeatherApiOptions>(
            configuration.GetSection("Weatherstack"));
        services.Configure<RedisOptions>(
            configuration.GetSection("Redis"));

        // Redis connection
        var redisConnection = configuration.GetValue<string>("Redis:Connection") ?? "localhost:6379";
        services.AddSingleton<IConnectionMultiplexer>(sp =>
            ConnectionMultiplexer.Connect(redisConnection));

        // Core Services
        services.AddTransient<IWeatherService, WeatherService>();
        services.AddTransient<IGeocodingService, GeocodingService>();

        // Factories
        services.AddSingleton<IWeatherServiceFactory, WeatherServiceFactory>();
        services.AddSingleton<IGeolocationServiceFactory, GeolocationServiceFactory>();

        // Weather provider clients (3rd party integrations)
        services.AddTransient<OpenWeatherMapServiceClient>();
        services.AddTransient<WeatherstackServiceClient>();
        services.AddTransient<OpenWeatherGeocodingServiceClient>();
        services.AddTransient<WeatherstackGeocodingServiceClient>();

        // HttpClient setup
        services.AddHttpClient<OpenWeatherMapServiceClient>((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<OpenMapWeatherApiOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        services.AddHttpClient<OpenWeatherGeocodingServiceClient>((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<OpenMapWeatherApiOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        services.AddHttpClient<WeatherstackServiceClient>((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<WeatherstackWeatherApiOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        services.AddHttpClient<WeatherstackGeocodingServiceClient>((provider, client) =>
        {
            var options = provider.GetRequiredService<IOptions<WeatherstackWeatherApiOptions>>().Value;
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        return services;
    }

    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        services.AddValidatorsFromAssembly(typeof(Program).Assembly);

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}