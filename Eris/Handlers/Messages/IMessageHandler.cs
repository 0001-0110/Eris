using Discord.WebSocket;

namespace Eris.Handlers.Messages;

public interface IMessageHandler : IMessageChannelHandler
{
    Task<bool> HandleMessage(SocketMessage message);
}
