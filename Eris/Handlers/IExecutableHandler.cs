using Discord.WebSocket;

namespace Eris.Handlers;

public interface IExecutableHandler
{
    bool IsEnabled(SocketGuild guild);
    Task<bool> IsEnabledAsync(SocketGuild guild);
}
