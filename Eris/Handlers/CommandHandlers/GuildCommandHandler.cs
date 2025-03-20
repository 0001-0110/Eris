using Discord;

namespace Eris.Handlers.CommandHandlers;

public abstract class GuildCommandHandler : BaseCommandHandler, IMessageChannelHandler
{
    // TODO leave this up to the user
    public bool IsDMEnabled => true;

    public abstract bool IsEnabled(IGuild guild);
}
