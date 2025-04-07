# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

# Copy everything
COPY . .

# Restore dependencies
RUN dotnet restore DiscordPlayerCountBot.csproj

# Build the project
RUN dotnet build DiscordPlayerCountBot.csproj -c Release

# Publish the project
RUN dotnet publish DiscordPlayerCountBot.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app
COPY --from=build /app/publish .

ENV ISDOCKER=True
ENTRYPOINT ["dotnet", "DiscordPlayerCountBot.dll"]
