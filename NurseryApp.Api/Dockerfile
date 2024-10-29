# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["NurseryApp.Api/NurseryApp.Api.csproj", "NurseryApp.Api/"]
COPY ["NurseryApp.Application/NurseryApp.Application.csproj", "NurseryApp.Application/"]
COPY ["NurseryApp.Data/NurseryApp.Data.csproj", "NurseryApp.Data/"]
COPY ["NurseryApp.Core/NurseryApp.Core.csproj", "NurseryApp.Core/"]
RUN dotnet restore "NurseryApp.Api/NurseryApp.Api.csproj"

# Copy the remaining source code
COPY . .

# Build the application
WORKDIR "/src/NurseryApp.Api"
RUN dotnet build "NurseryApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Stage 2: Publish the application
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NurseryApp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Stage 3: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Set the working directory
WORKDIR /app

# Copy the published application
COPY --from=publish /app/publish .

# Create a non-root user and group
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser

# Create the wwwroot directory and set permissions
RUN mkdir -p /app/wwwroot && chown -R 755 appuser:appgroup /app/wwwroot

# Set ownership of the app directory
RUN chown -R appuser:appgroup /app

# Switch to the non-root user
USER appuser

# Expose ports
EXPOSE 8080
EXPOSE 8081

# Set the entry point
ENTRYPOINT ["dotnet", "NurseryApp.Api.dll"]
