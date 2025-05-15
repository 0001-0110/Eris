using Discord.WebSocket;

namespace Eris.Handlers.Messages;

internal class MessageManager : IMessageManager
{
    private readonly IServiceProvider _services;
    private readonly IEnumerable<IMessageHandler> _messageHandlers;

    public MessageManager(IEnumerable<IMessageHandler> messageHandlers)
    {
        _messageHandlers = messageHandlers;
    }

    public async Task HandleMessage(SocketMessage message)
    {
        // foreach (IMessageHandler messageHandler in _messageHandlers)
        // {
        //     if (message.Channel.ChannelType switch
        //         {
        //             Discord.ChannelType.DM => messageHandler.IsDMEnabled,
        //             Discord.ChannelType.Text => messageHandler.IsEnabled(((SocketGuildChannel)message.Channel).Guild),
        //             _ => throw new Exception("Something went really wrong"),
        //         })
        //     {
        //         // If the handler returns false, this message has been handled and we stop here
        //         if (!await messageHandler.HandleMessage(message))
        //             return;
        //     }
        // }
    }
}
