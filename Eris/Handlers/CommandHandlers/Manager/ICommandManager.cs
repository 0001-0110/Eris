using Discord.WebSocket;

namespace Eris.Handlers.CommandHandlers.Manager;

internal interface ICommandManager
{
    public Task InitCommands(DiscordSocketClient client);
}
