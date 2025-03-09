using Discord.WebSocket;

namespace Eris.Handlers.Commands;

internal interface ICommandManager : IHandlerManager<ICommandHandler>
{
    Task HandleCommand(SocketSlashCommand command);
}
