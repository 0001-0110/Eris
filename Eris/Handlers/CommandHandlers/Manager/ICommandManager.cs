using Discord.WebSocket;

namespace Eris.Handlers.CommandHandlers.Manager;

internal interface ICommandManager : IHandlerManager<BaseCommandHandler>
{
    Task CreateCommands(DiscordSocketClient client);

    Task HandleCommand(SocketSlashCommand command);
}
