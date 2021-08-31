using System;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    public Manager manager;
    public Discord.Discord discord;

    void Start()
    {
        discord = new Discord.Discord(881600361251622943, (UInt64)CreateFlags.NoRequireDiscord);
        UpdateActivity();
    }

    void Update()
    {
        discord.RunCallbacks();
    }

    public void UpdateActivity()
    {
        var activityManager = discord.GetActivityManager();
        var activity = new Activity
        {
            Details = $"Pity: {manager.wishAmount}",
            State = $"Going for {manager.wishCharacter}"
        };
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Result.Ok)
            {
                Debug.Log("[Discord] Updated.");
            }
            else
            {
                Debug.LogWarning("[Discord] Couldn't update!");
            }
        });
    }

    public void Disconnect()
    {
        discord.Dispose();
    }
}
