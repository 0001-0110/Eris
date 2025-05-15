using Discord.WebSocket;

namespace Eris.Handlers.Messages.BuiltIn;

public class IgnoreBotsMessageHandler : IMessageHandler
{
    public Task<bool> Handle(SocketMessage message)
    {
        return Task.FromResult(message.Author.IsBot);
    }
}
