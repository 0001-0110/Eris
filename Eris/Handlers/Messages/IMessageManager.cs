using Discord.WebSocket;

namespace Eris.Handlers.Messages;

internal interface IMessageManager : IHandlerManager<IMessageHandler>
{
    Task HandleMessage(SocketMessage message);
}
