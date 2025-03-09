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
    private Task _serviceTask = Task.CompletedTask;

    private readonly TaskCompletionSource _shutdownSource;

    public DiscordSocketClient Client => _client;

    public ErisClient()
    {
        _cancellationTokenSource = new CancellationTokenSource();

        _client = new DiscordSocketClient(
            // TODO Be more restrictive depending on what is actually used
            new DiscordSocketConfig() { GatewayIntents = GatewayIntents.All });

        _commandManager = new CommandManager();
        _client.SlashCommandExecuted += _commandManager.HandleCommand;
        _messageManager = new MessageManager();
        _client.MessageReceived += _messageManager.HandleMessage;
        _serviceManager = new ServiceManager();

        _shutdownSource = new TaskCompletionSource();

        _client.Log += log => { Console.WriteLine(log.Message); return Task.CompletedTask; };
    }

    private async Task Connect()
    {
        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_TOKEN"));
        await _client.StartAsync();
    }

    private async Task Disconnect()
    {
        await _client.SetStatusAsync(UserStatus.Offline);
        await _client.StopAsync();
        await _client.LogoutAsync();
    }

    public async Task Start()
    {
        await Connect();
        //_serviceTask = _serviceManager.StartServices(_cancellationTokenSource.Token);
        await _shutdownSource.Task;
    }

    public async Task Stop()
    {
        _cancellationTokenSource.Cancel();
        await _serviceTask;
        await Disconnect();
        _shutdownSource.SetResult();
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
