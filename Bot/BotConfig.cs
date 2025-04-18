﻿namespace DiscordPlayerCountBot.Bot;

public class BotConfig
{
    public int UpdateTime { get; set; }
    public List<BotInformation> ServerInformation { get; set; } = [];
    public Dictionary<string, string> ApplicationTokens { get; set; } = [];
    public void CreateDefaults()
    {
        ServerInformation.Add(new()
        {
            Name = "TestBot",
            Address = "127.0.0.1:27014",
            Token = "DiscordTokenHere",
            Status = 0,
            UseNameAsLabel = false
        });
        UpdateTime = 30;

        ApplicationTokens.Add("SteamAPIKey", "Here");
        ApplicationTokens.Add("BattleMetricsKey", "Here");
    }
}