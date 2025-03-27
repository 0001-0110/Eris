using Discord.WebSocket;
using InjectoPatronum;

namespace Eris.Handlers.Messages;

internal class MessageManager : IMessageManager
{
    private readonly IDependencyInjector _injector;

    private readonly ICollection<Type> _messageHandlers;

    public MessageManager(IDependencyInjector injector)
    {
        _injector = injector;
        _messageHandlers = [];
    }

    public void AddHandler<TMessageHandler>() where TMessageHandler : IMessageHandler
    {
        _messageHandlers.Add(typeof(TMessageHandler));
    }

    public async Task HandleMessage(SocketMessage message)
    {
        foreach (IMessageHandler messageHandler in _messageHandlers
            .Select(type => (IMessageHandler)_injector.Instantiate(type)))
        {
            if (message.Channel.ChannelType switch
                {
                    Discord.ChannelType.DM => messageHandler.IsDMEnabled,
                    Discord.ChannelType.Text => messageHandler.IsEnabled(((SocketGuildChannel)message.Channel).Guild),
                    _ => throw new Exception("Something went really wrong"),
                })
            {
                // If the handler returns false, this message has been handled and we stop here
                if (!await messageHandler.HandleMessage(message))
                    return;
            }
        }
    }
}
