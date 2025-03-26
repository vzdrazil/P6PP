FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER 1000
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY src/BookingPayments.API/BookingPayments.API.csproj src/BookingPayments.API/
COPY libs/ReservationSystem.Shared/ReservationSystem.Shared.csproj libs/ReservationSystem.Shared/
RUN dotnet restore "src/BookingPayments.API/BookingPayments.API.csproj"
COPY . .
WORKDIR "/src/src/BookingPayments.API"
RUN dotnet build "BookingPayments.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BookingPayments.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookingPayments.API.dll"]