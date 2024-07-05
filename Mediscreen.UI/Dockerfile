FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Mediscreen.UI/Mediscreen.UI.csproj", "Mediscreen.UI/"]
RUN dotnet restore "./Mediscreen.UI/Mediscreen.UI.csproj"
COPY . .
WORKDIR "/src/Mediscreen.UI"
RUN dotnet build "./Mediscreen.UI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Mediscreen.UI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mediscreen.UI.dll"]