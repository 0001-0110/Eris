using Discord;
using Discord.WebSocket;

namespace Eris;

public class ErisClient
{
    private readonly CancellationTokenSource _cancellationTokenSource;
    private DiscordSocketClient _client;

    public ErisClient()
    {
        _cancellationTokenSource = new CancellationTokenSource();

        _client = new DiscordSocketClient(
            // TODO Be more restrictive depending on what is actually used
            new DiscordSocketConfig() { GatewayIntents = GatewayIntents.All });

        _client.Log += log => { Console.WriteLine(log.Message); return Task.CompletedTask; };
        // TODO Remove later
        _client.Ready += async () =>
        {
            await _client.SetStatusAsync(UserStatus.Online);
            await _client.SetGameAsync("test");
        };
    }

    public async Task Run()
    {
        await Connect();

        try
        {
            await Task.Delay(-1, _cancellationTokenSource.Token);
        }
        catch (TaskCanceledException exception)
        {
            // TODO Log
        }
    }

    private async Task Connect()
    {
        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_TOKEN"));
        await _client.StartAsync();

    }
}
