using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Services;
using WeatherForecasting.Application.Validators;
using WeatherForecasting.Controllers.Middleware;
using WeatherForecasting.Infrastructure.WeatherProviders;
using WeatherForecasting.Infrastructure.WeatherProviders.Common;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient;
using WeatherForecasting.Infrastructure.WeatherProviders.OpenWeatherMapClient.Models;
using WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient;
using WeatherForecasting.Infrastructure.WeatherProviders.WeatherstackClient.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging();
builder.Services.AddControllers();

// Register real services
builder.Services.AddTransient<OpenWeatherMapServiceClient>();
builder.Services.AddSingleton<WeatherstackServiceClient>();

builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
builder.Services.AddSingleton<IWeatherServiceFactory, WeatherServiceFactory>();
builder.Services.AddSingleton<IGeolocationServiceFactory, GeolocationServiceFactory>();

builder.Services.AddTransient<OpenWeatherGeocodingServiceClient>();
builder.Services.AddTransient<WeatherstackGeocodingServiceClient>();

builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddTransient<GeocodingService>();

RegisterMediator(builder);

builder.Services.Configure<OpenMapWeatherApiOptions>(
    builder.Configuration.GetSection("OpenWeatherMap"));

builder.Services.Configure<WeatherstackWeatherApiOptions>(
    builder.Configuration.GetSection("Weatherstack"));

builder.Services.Configure<RedisOptions>(
    builder.Configuration.GetSection("Redis"));

builder.Services.AddHttpClient<OpenWeatherMapServiceClient>((provider, client) =>
{
    var options = provider.GetRequiredService<IOptions<OpenMapWeatherApiOptions>>().Value;

    client.BaseAddress = new Uri(options.BaseUrl);
});

builder.Services.AddHttpClient<OpenWeatherGeocodingServiceClient>((provider, client) =>
{
    var options = provider.GetRequiredService<IOptions<OpenMapWeatherApiOptions>>().Value;

    client.BaseAddress = new Uri(options.BaseUrl);
});

builder.Services.AddHttpClient<WeatherstackServiceClient>((provider, client) =>
{
    var options = provider.GetRequiredService<IOptions<WeatherstackWeatherApiOptions>>().Value;

    client.BaseAddress = new Uri(options.BaseUrl);
});

builder.Services.AddHttpClient<WeatherstackGeocodingServiceClient>((provider, client) =>
{
    var options = provider.GetRequiredService<IOptions<WeatherstackWeatherApiOptions>>().Value;

    client.BaseAddress = new Uri(options.BaseUrl);
});

var redisConnection = builder.Configuration.GetValue<string>("Redis:Connection");

// Register the Redis connection multiplexer as a singleton
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(redisConnection));

builder.Services.AddSingleton<IWeatherServiceClient, OpenWeatherMapServiceClient>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ErrorHandlingMiddleware>();

await app.RunAsync();

void RegisterMediator(WebApplicationBuilder webApplicationBuilder)
{
    builder.Services.AddMediatR(cfg =>
    {
        cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    });

    builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
}