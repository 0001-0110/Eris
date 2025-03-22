using Discord;

namespace Eris.Logging;

public interface ILogger
{
    Task Log(LogMessage log);

    Task Log(LogSeverity severity, string source, string message, Exception exception = null!);
}
