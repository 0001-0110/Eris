using Discord.WebSocket;

namespace Eris.Handlers.Messages;

internal class MessageManager : IMessageManager
{
    private readonly ICollection<IMessageHandler> _messageHandlers;

    public MessageManager()
    {
        _messageHandlers = new List<IMessageHandler>();
    }

    public void AddHandler(IMessageHandler handler)
    {
        _messageHandlers.Add(handler);
    }

    public async Task HandleMessage(SocketMessage message)
    {
        foreach (IMessageHandler messageHandler in _messageHandlers)
            if (await messageHandler.IsEnabledAsync(message.Channel))
                await messageHandler.HandleMessage(message);
    }
}
