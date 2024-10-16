# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the csproj file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use the official .NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the build output to the runtime image
COPY --from=build /app/out ./

# Expose the port the app runs on
EXPOSE 80

# Set the entry point for the application
ENTRYPOINT ["dotnet", "DIYService.dll"]
