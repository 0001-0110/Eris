using Discord;

namespace Eris.Logging;

public abstract class LoggerBase : ILogger
{
    public abstract Task Log(LogMessage log);

    public Task Log(LogSeverity severity, string source, string message, Exception? exception = null)
    {
        return Log(new LogMessage(severity, source, message, exception));
    }
}
