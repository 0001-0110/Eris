using Discord.WebSocket;

namespace Eris.Handlers.CommandHandlers.Manager;

/// <summary>
/// Manages all command handlers.
/// </summary>
internal interface ICommandManager
{
    /// <summary>
    /// Initializes and registers all slash commands using the provided Discord client.
    /// This should be called once the client is ready.
    /// </summary>
    Task InitCommands(DiscordSocketClient client);
}
