using Discord.WebSocket;

namespace Eris.Handlers.Messages;

public interface IMessageHandler : IMessageChannelHandler
{
    Task HandleMessage(SocketMessage message);
}
