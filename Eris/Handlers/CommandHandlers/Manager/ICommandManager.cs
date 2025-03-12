using Discord.WebSocket;
using Eris.Handlers.CommandHandlers;

namespace Eris.Handlers.Commands.Manager;

internal interface ICommandManager : IHandlerManager<BaseCommandHandler>
{
    Task HandleCommand(SocketSlashCommand command);
}
