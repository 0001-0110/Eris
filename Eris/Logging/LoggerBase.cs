using Discord;

namespace Eris.Logging;

/// <summary>
/// Provides a base implementation of <see cref="ILogger"/> that converts severity-level logs into <see cref="LogMessage"/> objects.
/// </summary>
public abstract class LoggerBase : ILogger
{
    /// <inheritdoc/>
    public abstract Task Log(LogMessage log);

    /// <inheritdoc/>
    public Task Log(LogSeverity severity, string source, string message, Exception? exception = null)
    {
        return Log(new LogMessage(severity, source, message, exception));
    }
}
