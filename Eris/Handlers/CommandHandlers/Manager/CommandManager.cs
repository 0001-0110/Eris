using System.Reflection;
using Discord.Interactions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace Eris.Handlers.CommandHandlers.Manager;

internal class CommandManager : ICommandManager
{
    private readonly InteractionService _interactionService;
    private readonly IServiceProvider _services;

    public CommandManager(InteractionService interactionService)
    {
        _interactionService = interactionService;
        _services = new ServiceCollection().BuildServiceProvider(); //services;
    }

    public async Task InitCommands(DiscordSocketClient client)
    {
        // TODO How to allow for customization ?
        await _interactionService.AddModulesAsync(Assembly.GetExecutingAssembly(), _services);

        client.InteractionCreated += async interaction =>
        {
            SocketInteractionContext context = new SocketInteractionContext(client, interaction);
            await _interactionService.ExecuteCommandAsync(context, _services);
        };
    }
}
