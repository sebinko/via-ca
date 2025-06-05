# Multi-stage build for ASP.NET Core backend
# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

# Set environment to Release for optimization
ENV DOTNET_ENVIRONMENT=Production
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_SKIP_FIRST_TIME_EXPERIENCE=1

# Copy only the project file first and restore to optimize caching
COPY ["backend.csproj", "./"]
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Create runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app

# Create non-root user for security
RUN adduser --disabled-password --gecos "" --no-create-home appuser && \
    chown -R appuser /app

# Install curl for healthcheck
RUN apk --no-cache add curl

# Install ICU for globalization
RUN apk --no-cache add icu-libs

# Copy built app
COPY --from=build --chown=appuser:appuser /app/publish .

# Set environment variables
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    DOTNET_SYSTEM_GLOBALIZATION_USING_ICU=true \
    LANG=en_US.UTF-8 \
    LC_ALL=en_US.UTF-8
EXPOSE 80

# Switch to non-root user
USER appuser

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:80/health || exit 1

# Run the app
ENTRYPOINT ["dotnet", "backend.dll"]
