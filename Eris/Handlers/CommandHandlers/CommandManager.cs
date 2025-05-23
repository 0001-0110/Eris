using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Eris.Handlers.CommandHandlers;

/// <inheritdoc cref="ICommandManager"/>
internal class CommandManager : ICommandManager
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _interactionService;
    private readonly IServiceProvider _services;

    public CommandManager(DiscordSocketClient client, InteractionService interactionService, IServiceProvider services)
    {
        _client = client;
        _interactionService = interactionService;
        _services = services;
    }

    /// <inheritdoc/>
    public async Task InitCommands(DiscordSocketClient client)
    {
        // Delete all global commands
        await _interactionService.RegisterCommandsGloballyAsync(deleteMissing: true);

        foreach (CommandHandler commandHandler in _services.GetServices<CommandHandler>())
            await _interactionService.AddModuleAsync(commandHandler.GetType(), _services);

        // TODO Handle guild and global commands
        foreach (SocketGuild? guild in _client.Guilds)
            await _interactionService.RegisterCommandsToGuildAsync(guild.Id, deleteMissing: true);

        client.InteractionCreated += async interaction =>
        {
            SocketInteractionContext context = new SocketInteractionContext(client, interaction);
            await _interactionService.ExecuteCommandAsync(context, _services);
        };
    }
}
