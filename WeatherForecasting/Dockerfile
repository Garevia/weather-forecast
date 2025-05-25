FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
EXPOSE 5001

# Copy everything
COPY . ./

# Restore NuGet packages
RUN dotnet restore

# Build and publish
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/out ./
ENV ASPNETCORE_URLS=http://+:5001
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 80

ENTRYPOINT ["dotnet", "WeatherForecasting.dll"]
