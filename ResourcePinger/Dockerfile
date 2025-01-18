#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ResourcePinger/ResourcePinger.csproj", "ResourcePinger/"]
RUN dotnet restore "./ResourcePinger/./ResourcePinger.csproj"
COPY . .
WORKDIR "/src/ResourcePinger"
RUN dotnet build "./ResourcePinger.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ResourcePinger.csproj" -c $BUILD_CONFIGURATION -o /app/publish --self-contained true
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ResourcePinger.dll"]