using Discord;
using Discord.WebSocket;
using Eris.Handlers.Commands;
using Eris.Handlers.Messages;
using Eris.Handlers.Services;

namespace Eris;

public class ErisClient
{
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly DiscordSocketClient _client;

    private readonly ICommandManager _commandManager;
    private readonly IMessageManager _messageManager;
    private readonly IServiceManager _serviceManager;

    public ErisClient()
    {
        _cancellationTokenSource = new CancellationTokenSource();

        _client = new DiscordSocketClient(
            // TODO Be more restrictive depending on what is actually used
            new DiscordSocketConfig() { GatewayIntents = GatewayIntents.All });

        _commandManager = new CommandManager();
        _messageManager = new MessageManager();
        _serviceManager = new ServiceManager();

        _client.Log += log => { Console.WriteLine(log.Message); return Task.CompletedTask; };
        // TODO Remove later
        _client.Ready += async () =>
        {
            await _client.SetStatusAsync(UserStatus.Online);
            await _client.SetGameAsync("test");
        };
    }

    private async Task Connect()
    {
        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_TOKEN"));
        await _client.StartAsync();
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

    public ErisClient AddCommandHandler(ICommandHandler commandHandler)
    {
        _commandManager.AddHandler(commandHandler);
        return this;
    }

    public ErisClient AddMessageHandler(IMessageHandler messageHandler)
    {
        _messageManager.AddHandler(messageHandler);
        return this;
    }

    public ErisClient AddService(IServiceHandler service)
    {
        _serviceManager.AddHandler(service);
        return this;
    }
}
