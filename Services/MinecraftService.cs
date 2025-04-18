﻿using DiscordPlayerCountBot.Data.Minecraft;
using DiscordPlayerCountBot.Http;

namespace DiscordPlayerCountBot.Services;

public class MinecraftService : IMinecraftService
{
    public async Task<MinecraftServer?> GetMinecraftServerInformationAsync(string address, int port)
    {
        using var httpClient = new HttpExecuter();
        var additionalHeaders = new Dictionary<string, string>
        {
            { "User-Agent", $"pcdb/1.0 ({address}:{port})" }
        };

        return await httpClient.GET<object, MinecraftServer>($"https://api.mcsrvstat.us/2/{address}:{port}", additionalHeaders: additionalHeaders);
    }
}