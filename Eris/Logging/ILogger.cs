using Discord;

namespace Eris.Logging;

public interface ILogger
{
    Task Log(LogMessage log);
}
