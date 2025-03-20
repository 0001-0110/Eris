using Discord.WebSocket;

namespace Eris.Handlers.Messages;

internal class MessageManager : IMessageManager
{
    private readonly ICollection<IMessageHandler> _messageHandlers;

    public MessageManager()
    {
        _messageHandlers = new List<IMessageHandler>();
    }

    public void AddHandler<TMessageHandler>() where TMessageHandler : IMessageHandler
    {
        //_messageHandlers.Add<TMessageHandler>();
        throw new NotImplementedException();
    }

    public async Task HandleMessage(SocketMessage message)
    {
        foreach (IMessageHandler messageHandler in _messageHandlers)
        {
            if (message.Channel.ChannelType switch
                {
                    Discord.ChannelType.DM => messageHandler.IsDMEnabled,
                    Discord.ChannelType.Text => messageHandler.IsEnabled(((SocketGuildChannel)message.Channel).Guild),
                    _ => throw new Exception("Something went really wrong"),
                })
            {
                await messageHandler.HandleMessage(message);
            }
        }
    }
}
