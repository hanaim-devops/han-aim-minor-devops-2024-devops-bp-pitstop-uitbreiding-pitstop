FROM luukjp/pitstop-dotnet-aspnet-base:1.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM luukjp/pitstop-dotnet-sdk-base:1.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RentalManagementEventHandler/RentalManagementEventHandler.csproj", "RentalManagementEventHandler/"]
RUN dotnet restore "RentalManagementEventHandler/RentalManagementEventHandler.csproj"
COPY . .
WORKDIR "/src/RentalManagementEventHandler"
RUN dotnet build "RentalManagementEventHandler.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RentalManagementEventHandler.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RentalManagementEventHandler.dll"]
