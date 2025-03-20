using Discord;
using Discord.WebSocket;
using Eris.Handlers.CommandHandlers;
using Eris.Handlers.CommandHandlers.Manager;
using Eris.Handlers.Messages;
using Eris.Handlers.Services;
using InjectoPatronum;

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

        // TODO Improve dependency injection
        _commandManager = new CommandManager(new DependencyInjector());
        _client.SlashCommandExecuted += _commandManager.HandleCommand;
        _messageManager = new MessageManager();
        _client.MessageReceived += _messageManager.HandleMessage;
        _serviceManager = new ServiceManager();

        _shutdownSource = new TaskCompletionSource();

        _client.Ready += OnReady;
        _client.Log += log => { Console.WriteLine(log.Message); return Task.CompletedTask; };
    }

    private async Task Connect()
    {
        await _client.LoginAsync(TokenType.Bot, Environment.GetEnvironmentVariable("DISCORD_TOKEN"));
        await _client.StartAsync();
    }

    private async Task OnReady()
    {
        await _commandManager.CreateCommands(_client);
        //_serviceTask = _serviceManager.StartServices(_cancellationTokenSource.Token);
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

    public ErisClient AddCommandHandler<TCommandHandler>() where TCommandHandler : BaseCommandHandler
    {
        _commandManager.AddHandler<TCommandHandler>();
        return this;
    }

    public ErisClient AddMessageHandler<TMessageHandler>() where TMessageHandler : IMessageHandler
    {
        _messageManager.AddHandler<TMessageHandler>();
        return this;
    }

    public ErisClient AddService<TServiceHandler>() where TServiceHandler : IServiceHandler
    {
        _serviceManager.AddHandler<TServiceHandler>();
        return this;
    }
}
