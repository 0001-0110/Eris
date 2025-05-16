using Discord;
using Discord.WebSocket;
using Eris.Handlers.CommandHandlers.Manager;
using Eris.Handlers.Messages;
using Eris.Handlers.Services;
using Eris.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Eris;

/// <summary>
/// Represents the core Discord bot client that handles command, message, and service management.
/// This class should be constructed using <see cref="ErisClientBuilder"/>.
/// </summary>
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

    /// <summary>
    /// Gets the underlying Discord socket client.
    /// </summary>
    public DiscordSocketClient Client => _client;

    /// <summary>
    /// DO NOT use this constructor directly. This public constructor exists only to support dependency injection.
    /// Use <see cref="ErisClientBuilder"/> to create and configure an instance of <see cref="ErisClient"/>.
    /// </summary>
    /// <param name="services">A configured service provider with all required dependencies.</param>
    [Obsolete("Use the ErisClientBuilder instead", true)]
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
        _client.MessageReceived += _messageManager.Handle;
        _serviceManager = serviceManager;

        _shutdownSource = new TaskCompletionSource();

        _client.Ready += OnReady;
    }

    /// <summary>
    /// Connects the client to Discord using the token from the DISCORD_TOKEN environment variable.
    /// </summary>
    private async Task Connect()
    {
        // TODO Searching in the var env is not the best way. Using IOptions would be better
        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_TOKEN"));
        await _client.StartAsync();
    }

    /// <summary>
    /// Event handler for the client’s Ready event.
    /// Initializes command registration and starts background services.
    /// </summary>
    private async Task OnReady()
    {
        await _commandManager.InitCommands(_client);
        _serviceTask = _serviceManager.StartServices(_cancellationTokenSource.Token);
    }

    /// <summary>
    /// Gracefully disconnects the client from Discord.
    /// </summary>
    private async Task Disconnect()
    {
        await _client.SetStatusAsync(UserStatus.Offline);
        await _client.StopAsync();
        await _client.LogoutAsync();
    }

    /// <summary>
    /// Starts the bot and blocks until a shutdown is triggered via <see cref="Stop"/>.
    /// </summary>
    public async Task Run()
    {
        await Connect();
        await _shutdownSource.Task;
    }

    /// <summary>
    /// Triggers shutdown of the bot, cancelling background services and disconnecting from Discord.
    /// </summary>
    public async Task Stop()
    {
        _cancellationTokenSource.Cancel();
        await _serviceTask;
        await Disconnect();
        _shutdownSource.SetResult();
    }
}
