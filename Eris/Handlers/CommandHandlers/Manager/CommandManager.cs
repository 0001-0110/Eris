using Discord.WebSocket;

namespace Eris.Handlers.Commands.Manager;

internal class CommandManager : ICommandManager
{
    public void AddHandler(CommandHandler handler)
    {
        throw new NotImplementedException();
    }

    public Task HandleCommand(SocketSlashCommand command)
    {
        throw new NotImplementedException();
    }
}
