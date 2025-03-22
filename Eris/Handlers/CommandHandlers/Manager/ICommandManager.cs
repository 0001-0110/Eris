using Discord.WebSocket;

namespace Eris.Handlers.CommandHandlers.Manager;

internal interface ICommandManager : IHandlerManager<RootCommandHandler>
{
    Task CreateCommands(DiscordSocketClient client);

    Task HandleCommand(SocketSlashCommand command);
}
