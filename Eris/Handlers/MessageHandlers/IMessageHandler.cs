using Discord.WebSocket;

namespace Eris.Handlers.MessageHandlers;

/// <summary>
/// Represents a message handler in a chain-of-responsibility pattern.
/// Each handler can choose to process a message and optionally stop further propagation.
/// </summary>
public interface IMessageHandler
{
    /// <summary>
    /// Handles an incoming Discord socket message.
    /// </summary>
    /// <param name="message">The received message to process.</param>
    /// <returns>
    /// A task that resolves to <c>true</c> if the handler processed the message
    /// and no further handlers should be called; otherwise, <c>false</c> to continue the chain.
    /// </returns>
    Task<bool> Handle(SocketMessage message);
}
