FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RentalCarManagementAPI/RentalCarManagementAPI.csproj", "RentalCarManagementAPI/"]
RUN dotnet restore "RentalCarManagementAPI/RentalCarManagementAPI.csproj"
COPY . .
WORKDIR "/src/RentalCarManagementAPI"
RUN dotnet build "RentalCarManagementAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RentalCarManagementAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentalCarManagementAPI.dll"]
