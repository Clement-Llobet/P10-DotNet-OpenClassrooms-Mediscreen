FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Mediscreen.API/Mediscreen.API.csproj", "Mediscreen.API/"]
COPY ["Mediscreen.Infrastructure/Mediscreen.Infrastructure.csproj", "Mediscreen.Infrastructure/"]
RUN dotnet restore "./Mediscreen.API/Mediscreen.API.csproj"
COPY . .
WORKDIR "/src/Mediscreen.API"
RUN dotnet build "./Mediscreen.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Mediscreen.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mediscreen.API.dll"]