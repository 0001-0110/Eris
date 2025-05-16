using Discord.Interactions;

namespace Eris.Handlers.CommandHandlers;

/// <summary>
/// Base class for all slash command handlers in Eris.
/// Inherit from this class to implement your own command modules using Discord's interaction framework.
/// </summary>
public abstract class CommandHandler : InteractionModuleBase<SocketInteractionContext>
{
}
