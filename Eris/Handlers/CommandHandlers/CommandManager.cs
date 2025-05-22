using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Eris.Handlers.CommandHandlers;

/// <inheritdoc cref="ICommandManager"/>
internal class CommandManager : ICommandManager
{
    private readonly InteractionService _interactionService;
    private readonly IServiceProvider _services;

    public CommandManager(InteractionService interactionService, IServiceProvider services)
    {
        _interactionService = interactionService;
        _services = services;
    }

    /// <inheritdoc/>
    public async Task InitCommands(DiscordSocketClient client)
    {
        foreach (CommandHandler commandHandler in _services.GetServices<CommandHandler>())
            await _interactionService.AddModuleAsync(commandHandler.GetType(), _services);

        // TODO Handle guild and global commands
        // await _interactionService.RegisterCommandsToGuildAsync(854747950973452288, deleteMissing: true);
        await _interactionService.RegisterCommandsGloballyAsync(deleteMissing: true);

        client.InteractionCreated += async interaction =>
        {
            SocketInteractionContext context = new SocketInteractionContext(client, interaction);
            await _interactionService.ExecuteCommandAsync(context, _services);
        };
    }
}
