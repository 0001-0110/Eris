using Discord.WebSocket;

namespace Eris.Handlers.MessageHandlers.BuiltIn;

/// <summary>
/// A message handler that echoes back any received message content to the same channel.
/// </summary>
public class EchoMessageHandler : IMessageHandler
{
    /// <summary>
    /// Sends back the same message content to the channel where it was received.
    /// Returns <c>true</c> to indicate that the message has been handled and should not be passed further down the chain.
    /// </summary>
    /// <param name="message">The incoming Discord socket message.</param>
    /// <returns><c>true</c> to stop further handling of the message.</returns>
    public async Task<bool> Handle(SocketMessage message)
    {
        await message.Channel.SendMessageAsync(message.Content);
        return true;
    }
}
