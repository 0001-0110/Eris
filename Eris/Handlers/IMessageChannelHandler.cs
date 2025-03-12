using Discord.WebSocket;

namespace Eris.Handlers;

public interface IMessageChannelHandler : IHandler
{
    Task<bool> IsEnabledAsync(ISocketMessageChannel channel);
}
