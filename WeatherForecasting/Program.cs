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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging();
builder.Services.AddControllers();

// Register real services
builder.Services.AddTransient<OpenWeatherMapServiceClient>();
builder.Services.AddSingleton<WeatherstackServiceClient>();
builder.Services.AddSingleton<OpenWeatherGeocodingServiceClient>();
builder.Services.AddSingleton<WeatherstackGeocodingServiceClient>();

builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
builder.Services.AddSingleton<IWeatherServiceFactory, WeatherServiceFactory>();
builder.Services.AddSingleton<IGeolocationServiceFactory, GeolocationServiceFactory>();
builder.Services.AddSingleton<IGeolocationServiceFactory, GeolocationServiceFactory>();

builder.Services.AddTransient<IGeocodingServiceClient, OpenWeatherGeocodingServiceClient>();
builder.Services.AddTransient<IGeocodingServiceClient, WeatherstackGeocodingServiceClient>();
builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddTransient<IGeocodingService, GeocodingService>();

RegisterMediator(builder);

builder.Services.Configure<WeatherApiOptions>(
    builder.Configuration.GetSection("OpenWeatherMap"));

builder.Services.Configure<RedisOptions>(
    builder.Configuration.GetSection("Redis"));

builder.Services.AddHttpClient<OpenWeatherMapServiceClient>((provider, client) =>
{
    var options = provider.GetRequiredService<IOptions<WeatherApiOptions>>().Value;

    client.BaseAddress = new Uri(options.BaseUrl);
});

builder.Services.AddHttpClient<OpenWeatherGeocodingServiceClient>((provider, client) =>
{
    var options = provider.GetRequiredService<IOptions<WeatherApiOptions>>().Value;

    client.BaseAddress = new Uri(options.BaseUrl);
});

var redisConnectionString = builder.Configuration.GetConnectionString("Redis") ?? "localhost:6379";

// Register the Redis connection multiplexer as a singleton
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(redisConnectionString));

var redis = ConnectionMultiplexer.Connect("localhost:6379");
builder.Services.AddSingleton(redis);

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