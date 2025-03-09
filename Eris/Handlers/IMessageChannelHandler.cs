using Discord.WebSocket;

namespace Eris.Handlers;

public interface IMessageChannelHandler : IHandler
{
    bool IsEnabled(ISocketMessageChannel channel);

    Task<bool> IsEnabledAsync(ISocketMessageChannel channel);
}
