using Discord.WebSocket;

namespace Eris.Handlers.MessageHandlers.BuiltIn;

/// <summary>
/// A message handler that filters out messages sent by bots.
/// </summary>
public class IgnoreBotsMessageHandler : IMessageHandler
{
    /// <summary>
    /// Returns <c>true</c> if the message author is a bot, indicating the message should be ignored.
    /// </summary>
    /// <param name="message">The incoming Discord socket message.</param>
    /// <returns><c>true</c> if the message is from a bot and should be ignored; otherwise, <c>false</c>.</returns>
    public Task<bool> Handle(SocketMessage message)
    {
        return Task.FromResult(message.Author.IsBot);
    }
}
