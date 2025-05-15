using Discord.WebSocket;

namespace Eris.Handlers.Messages.BuiltIn;

public class EchoMessageHandler : IMessageHandler
{
    public async Task<bool> Handle(SocketMessage message)
    {
        await message.Channel.SendMessageAsync(message.Content);
        return true;
    }
}
