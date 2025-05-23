using Microsoft.Extensions.Options;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Application.Services;
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
builder.Services.AddSingleton<OpenWeatherMapServiceClient>();
builder.Services.AddSingleton<WeatherstackServiceClient>();
builder.Services.AddSingleton<OpenWeatherGeocodingServiceClient>();
builder.Services.AddSingleton<WeatherstackGeocodingServiceClient>();

builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
builder.Services.AddSingleton<IWeatherServiceFactory, WeatherServiceFactory>();
builder.Services.AddSingleton<IGeocodingServiceClient, OpenWeatherGeocodingServiceClient>();
builder.Services.AddSingleton<IGeocodingServiceClient, WeatherstackGeocodingServiceClient>();
builder.Services.AddSingleton<IWeatherService, WeatherService>();

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(GetWeatherForecastByCityHandler).Assembly));
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(GetWeatherForecastByLonAndLatHandler).Assembly));
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(GetWeatherForecastForFiveDaysHandler).Assembly));
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(GetGeocodingHandler).Assembly));

builder.Services.Configure<WeatherApiOptions>(
    builder.Configuration.GetSection("OpenWeatherMap"));

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

await app.RunAsync();