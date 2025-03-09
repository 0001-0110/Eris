using Discord.WebSocket;

namespace Eris.Handlers.Commands;

internal class CommandManager : ICommandManager
{
    public void AddHandler(ICommandHandler handler)
    {
        throw new NotImplementedException();
    }

    public Task HandleCommand(SocketSlashCommand command)
    {
        throw new NotImplementedException();
    }
}
