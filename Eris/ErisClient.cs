using Discord;
using Discord.WebSocket;
using Eris.Handlers.CommandHandlers.Manager;
using Eris.Handlers.Messages;
using Eris.Handlers.Services;
using Eris.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Eris;

public class ErisClient
{
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly DiscordSocketClient _client;

    private readonly ILogger _logger;

    private readonly ICommandManager _commandManager;
    private readonly IMessageManager _messageManager;
    private readonly IServiceManager _serviceManager;
    private Task _serviceTask = Task.CompletedTask;

    private readonly TaskCompletionSource _shutdownSource;

    public DiscordSocketClient Client => _client;

    // DI doesn't work with internal ctors, but the other ctor can't be made public since it uses internal classes
    public ErisClient(IServiceProvider services) : this(
        services.GetRequiredService<DiscordSocketClient>(),
        services.GetRequiredService<ILogger>(),
        services.GetRequiredService<ICommandManager>(),
        services.GetRequiredService<IMessageManager>(),
        services.GetRequiredService<IServiceManager>()
    )
    { }

    internal ErisClient(DiscordSocketClient client, ILogger logger, ICommandManager commandManager,
        IMessageManager messageManager, IServiceManager serviceManager)
    {
        _cancellationTokenSource = new CancellationTokenSource();

        _client = client;
        _logger = logger;
        _client.Log += _logger.Log;
        _commandManager = commandManager;
        _messageManager = messageManager;
        _client.MessageReceived += _messageManager.HandleMessage;
        _serviceManager = serviceManager;

        _shutdownSource = new TaskCompletionSource();

        _client.Ready += OnReady;
    }

    private async Task Connect()
    {
        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_TOKEN"));
        await _client.StartAsync();
    }

    private async Task OnReady()
    {
        await _commandManager.InitCommands(_client);
        _serviceTask = _serviceManager.StartServices(_cancellationTokenSource.Token);
    }

    private async Task Disconnect()
    {
        await _client.SetStatusAsync(UserStatus.Offline);
        await _client.StopAsync();
        await _client.LogoutAsync();
    }

    public async Task Run()
    {
        await Connect();
        await _shutdownSource.Task;
    }

    public async Task Stop()
    {
        _cancellationTokenSource.Cancel();
        await _serviceTask;
        await Disconnect();
        _shutdownSource.SetResult();
    }
}
