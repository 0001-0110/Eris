using Discord.WebSocket;

namespace Eris.Handlers.MessageHandlers;

/// <inheritdoc cref="IMessageManager"/>
internal class MessageManager : IMessageManager
{
    private readonly IEnumerable<IMessageHandler> _messageHandlers;

    public MessageManager(IEnumerable<IMessageHandler> messageHandlers)
    {
        _messageHandlers = messageHandlers;
    }

    /// <inheritdoc/>
    public async Task Handle(SocketMessage message)
    {
        foreach (IMessageHandler messageHandler in _messageHandlers)
        {
            // true means that this message has been handled, and the propagation should be stopped
            if (await messageHandler.Handle(message))
                return;
        }
    }
}
