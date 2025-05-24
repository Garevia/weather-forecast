# 🌦️ Weather Forecast API

A modular, extensible Weather Forecasting API built with ASP.NET Core. Designed using Domain-Driven Design and Clean Architecture principles, this solution supports multiple weather providers, robust validation, caching, and ease of extension — tailored for a senior software engineering interview.

---

## ✅ Features

- 🔌 **Pluggable Weather Providers**: Easily switch between OpenWeatherMap and Weatherstack via a provider factory.
- 🧭 **Geolocation Support**: Resolve city/country names to coordinates using provider-specific geocoding.
- 🧠 **Domain-Driven Design**: Clear separation of concerns across Domain, Application, Infrastructure, and API layers.
- ⚡ **Redis Caching**: Reduce third-party calls with smart caching based on coordinates and units.
- 🧪 **Request Validation**: Strong validation of inputs using FluentValidation and MediatR pipeline behavior.
- 🛡️ **Resilience Ready**: Designed for extensibility with retry policies and fault handling.
- 📚 **Swagger UI**: API self-documentation with request/response examples.

---

## 🏗️ Architecture

```
/WeatherForecasting
├── Api                  # ASP.NET Core Web API layer
├── Application          # Use cases, DTOs, handlers, interfaces
├── Domain               # Core business logic and models
├── Infrastructure       # 3rd-party integrations, persistence, caching
├── Tests                # Unit and integration tests
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Redis](https://redis.io/) running locally or via Docker

### Redis (local setup using Docker)

```bash
docker run -d -p 6379:6379 --name redis redis
```

### Run the API

```bash
cd WeatherForecasting.Api
dotnet run
```

### Access Swagger

```
http://localhost:5000/swagger
```

---

## ⚙️ Configuration

Update `appsettings.json` or `appsettings.Development.json`:

```json
"WeatherApi": {
  "Provider": "OpenWeather", // or "Weatherstack"
  "BaseUrl": "https://api.openweathermap.org/",
  "ApiKey": "your-api-key"
},
"Redis": {
  "ConnectionString": "localhost:6379",
  "CacheDuration": "00:30:00" // TimeSpan format
}
```

---

## 🧪 Testing

```bash
cd Tests
dotnet test
```

---

## 🧰 Technologies Used

- ASP.NET Core Web API
- MediatR
- FluentValidation
- Redis (via `StackExchange.Redis`)
- Swagger / Swashbuckle
- Clean Architecture / DDD

---

## ➕ Extending

To add a new weather provider:

1. Implement `IWeatherServiceClient` and `IGeocodingServiceClient`
2. Register the client in DI
3. Update the `WeatherServiceFactory` and `GeolocationServiceFactory`
4. Add provider-specific settings in `appsettings.json`

---

## 👤 Author

Tigran Balanyan
Senior Software Engineer | https://www.linkedin.com/in/tigran-balanyan-ab093a2a2/
