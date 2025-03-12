using Discord.WebSocket;

namespace Eris.Handlers.CommandHandlers;

public abstract class GuildCommandHandler : BaseCommandHandler, IMessageChannelHandler
{
    public abstract Task<bool> IsEnabledAsync(ISocketMessageChannel channel);
}
