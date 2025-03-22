using Discord;

namespace Eris.Handlers.CommandHandlers;

public abstract class GuildCommandHandler : RootCommandHandler, IMessageChannelHandler
{
    // TODO leave this up to the user
    public bool IsDMEnabled => true;

    public abstract bool IsEnabled(IGuild guild);
}
