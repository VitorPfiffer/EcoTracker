# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EcoTracker.API/EcoTracker.API.csproj", "EcoTracker.API/"]
COPY ["EcoTracker.Application/EcoTracker.Application.csproj", "EcoTracker.Application/"]
COPY ["EcoTracker.Core/EcoTracker.Core.csproj", "EcoTracker.Core/"]
COPY ["EcoTracker.Domain/EcoTracker.Domain.csproj", "EcoTracker.Domain/"]
COPY ["EcoTracker.Infrastructure/EcoTracker.Infrastructure.csproj", "EcoTracker.Infrastructure/"]

RUN dotnet restore "./EcoTracker.API/EcoTracker.API.csproj"
COPY . .
WORKDIR "/src/EcoTracker.API"
RUN dotnet build "./EcoTracker.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EcoTracker.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcoTracker.API.dll"]
