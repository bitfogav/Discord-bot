# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

# Copy everything
COPY . .

# Restore and publish
WORKDIR /src/DiscordPlayerCountBot
RUN dotnet restore
RUN dotnet publish DiscordPlayerCountBot.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app
COPY --from=build /app/publish .

ENV ISDOCKER=True
ENTRYPOINT ["dotnet", "DiscordPlayerCountBot.dll"]
