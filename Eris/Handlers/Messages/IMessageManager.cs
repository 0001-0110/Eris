using Discord.WebSocket;

namespace Eris.Handlers.Messages;

/// <summary>
/// Manages message handlers and routes incoming messages through the processing chain.
/// </summary>
internal interface IMessageManager
{
    /// <summary>
    /// Handles a received <see cref="SocketMessage"/> by passing it through the registered message handlers.
    /// </summary>
    /// <param name="message">The incoming message from Discord.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Handle(SocketMessage message);
}
