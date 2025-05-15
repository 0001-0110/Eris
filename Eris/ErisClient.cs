using Discord;
using Discord.Interactions;
using Discord.Rest;
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

    internal ErisClient(IServiceCollection services)
    {
        _cancellationTokenSource = new CancellationTokenSource();

        _client = new DiscordSocketClient(
            // TODO Be more restrictive depending on what is actually used
            new DiscordSocketConfig() { GatewayIntents = GatewayIntents.All });

        IServiceProvider provider = services
            .AddSingleton<IRestClientProvider>(_client)
            .AddSingleton<InteractionService>()
            .BuildServiceProvider();

        _logger= provider.GetRequiredService<ILogger>();
        _client.Log += _logger.Log;
        _commandManager = ActivatorUtilities.CreateInstance<CommandManager>(provider);
        _messageManager = ActivatorUtilities.CreateInstance<MessageManager>(provider);
        _client.MessageReceived += _messageManager.HandleMessage;
        _serviceManager = ActivatorUtilities.CreateInstance<ServiceManager>(provider);

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
