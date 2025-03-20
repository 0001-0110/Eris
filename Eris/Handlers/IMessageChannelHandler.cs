using Discord;

namespace Eris.Handlers;

public interface IMessageChannelHandler : IHandler
{
    /// <summary>
    /// Is this handler enabled in direct message channels ?
    /// </summary>
    bool IsDMEnabled { get; }

    bool IsEnabled(IGuild guild);
}
