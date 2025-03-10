FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5188

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/ExampleService.API/ExampleService.API.csproj", "src/ExampleService.API/"]
COPY ["libs/ReservationSystem.Shared/ReservationSystem.Shared.csproj", "libs/ReservationSystem.Shared/"]
RUN dotnet restore "src/ExampleService.API/ExampleService.API.csproj"
COPY . .
WORKDIR "/src/src/ExampleService.API"
RUN dotnet build "ExampleService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ExampleService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExampleService.API.dll"]
