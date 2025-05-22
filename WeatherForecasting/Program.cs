using Microsoft.Extensions.Options;
using WeatherForecasting.Application.Interfaces;
using WeatherForecasting.Application.Queries;
using WeatherForecasting.Application.Services;
using WeatherForecasting.Infrastructure.WeatherProviders;
using WeatherForecasting.Infrastructure.WeatherProviders.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging();
builder.Services.AddControllers();

// Register real services
builder.Services.AddSingleton<OpenWeatherMapService>();
builder.Services.AddSingleton<WeatherstackService>();
builder.Services.AddSingleton<ILoggerFactory, LoggerFactory>();
builder.Services.AddSingleton<IWeatherServiceFactory, WeatherServiceFactory>();
builder.Services.AddSingleton<IOpenWeatherGeocodingService, OpenWeatherGeocodingService>();

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(GetWeatherForecastByCityHandler).Assembly));
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(GetWeatherForecastByLonAndLatHandler).Assembly));
builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(GetWeatherForecastForFiveDaysHandler).Assembly));

builder.Services.Configure<WeatherApiOptions>(
    builder.Configuration.GetSection("OpenWeatherMap"));

builder.Services.AddHttpClient<OpenWeatherMapService>((provider, client) =>
{
    var options = provider.GetRequiredService<IOptions<WeatherApiOptions>>().Value;

    client.BaseAddress = new Uri(options.BaseUrl);
});

builder.Services.AddSingleton<IWeatherService, OpenWeatherMapService>();

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