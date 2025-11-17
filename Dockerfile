# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy everything
COPY . .

# Restore dependencies
RUN dotnet restore

# Build project in Release mode
RUN dotnet publish -c Release -o out

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published build
COPY --from=build /app/out .

# Expose port (Render uses 10000 internally)
EXPOSE 8080

# Start the application
ENTRYPOINT ["dotnet", "ApplicationTrackingSystem.dll"]
