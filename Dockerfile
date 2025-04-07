# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything into /app
COPY . .

# Publish the project from the correct path
RUN dotnet publish DiscordPlayerCountBot/DiscordPlayerCountBot.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Set entrypoint to run the app
ENTRYPOINT ["dotnet", "DiscordPlayerCountBot.dll"]

