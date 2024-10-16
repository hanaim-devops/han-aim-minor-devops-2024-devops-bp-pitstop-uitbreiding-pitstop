FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MaintanenceHistoryEventHandler.csproj", "MaintanenceHistoryEventHandler/"]
RUN dotnet restore "MaintanenceHistoryEventHandler/MaintanenceHistoryEventHandler.csproj"
WORKDIR "/src/MaintanenceHistoryEventHandler"
COPY . .
RUN dotnet build "MaintanenceHistoryEventHandler.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MaintanenceHistoryEventHandler.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pitstop.MaintanenceHistoryEventHandler.dll"]
