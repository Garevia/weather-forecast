# 🌦️ Weather Forecast API

A modular, extensible Weather Forecasting API built with ASP.NET Core. Designed using Domain-Driven Design and Clean Architecture principles, this solution supports multiple weather providers, robust validation, caching, and ease of extension — tailored for a senior software engineering interview.

---

## ✅ Features

- 🔌 **Pluggable Weather Providers**: Easily switch between OpenWeatherMap and Weatherstack via a provider factory.
- 🧭 **Geolocation Support**: Resolve city/country names to coordinates using provider-specific geocoding.
- 🧠 **Domain-Driven Design**: Clear separation of concerns across Domain, Application, Infrastructure, and API layers.
- ⚡ **Redis Caching**: Reduce third-party calls with smart caching based on coordinates and units.
- 🧪 **Request Validation**: Strong validation of inputs using FluentValidation and MediatR pipeline behavior.
- 📚 **Swagger UI**: API self-documentation with request/response examples.

---

## 🏗️ Architecture

```
/WeatherForecasting
├── Controllers          # ASP.NET Core Web API layer
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
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)

### Redis (local setup using Docker)

```bash
docker run --name my-redis -p 6379:6379 -d redis:latest
```

### Run the API

```bash
cd WeatherForecasting.Api
dotnet run
```

### 🔧 Build and Run Together

```bash
# Build and start the containers
docker-compose up --build
```

### 🔧 Stop containers without removing them
```bash
docker-compose stop
```

### 🔧 Or stop and remove containers
```bash
docker-compose down
```

### Access Swagger

```
http://localhost:5001/swagger
```

---

## ⚙️ Configuration

Update `appsettings.json` or `appsettings.Development.json`:

```json
 "OpenWeatherMap": {
  "ApiKey": "#OpenWeatherMapApiKey#",
  "BaseUrl": "#OpenWeatherUrl#"
},
"Weatherstack": {
  "ApiKey": "#WeatherstackApiKey#",
  "BaseUrl": "#WeatherstackUrl#"
},
"Redis": {
  "Connection": "localhost:6379,abortConnect=false",
  "TimeSpan": "00:15:00"
},
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
