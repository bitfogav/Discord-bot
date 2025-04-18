﻿using DiscordPlayerCountBot.Attributes;
using DiscordPlayerCountBot.Bot;
using DiscordPlayerCountBot.Enums;
using DiscordPlayerCountBot.Providers.Base;
using DiscordPlayerCountBot.Services;
using DiscordPlayerCountBot.ViewModels;

namespace DiscordPlayerCountBot.Providers;

[Obsolete("Found to be worse than Battle Metrics", true)]
[Name("Scum")]
public class ScumProvider : ServerInformationProvider
{
    public override DataProvider GetRequiredProviderType()
    {
        return DataProvider.SCUM;
    }

    public async override Task<BaseViewModel?> GetServerInformation(BotInformation information, Dictionary<string, string> applicationVariables)
    {
        var service = new ScumService();
        var addressAndPort = information.GetAddressAndPort();

        try
        {
            var apiResponse = await service.GetPlayerInformationAsync(addressAndPort.Item1, addressAndPort.Item2)
                ?? throw new ApplicationException("Response cannot be null.");

            if (apiResponse.Servers == 0)
                throw new ApplicationException("Response contained no valid servers.");

            var server = apiResponse.GetScumServerData(addressAndPort.Item2)
                ?? throw new ApplicationException("Could not find Server in Scum Provider.");

            HandleLastException(information);

            return new()
            {
                Address = addressAndPort.Item1,
                Port = addressAndPort.Item2,
                Players = server.Players,
                MaxPlayers = server.MaxPlayers,
                QueuedPlayers = 0
            };
        }
        catch (Exception e)
        {
            HandleException(e, information.Id.ToString());
            return null;
        }
    }
}