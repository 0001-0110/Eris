using Discord;

namespace Eris.Logging;

/// <summary>
/// Represents a logging interface used for capturing and recording log messages from the application and Discord client.
/// </summary>
public interface ILogger
{
    /// <summary>
    /// Logs a <see cref="LogMessage"/>.
    /// </summary>
    /// <param name="log">The log message to record.</param>
    /// <returns>A task that represents the asynchronous logging operation.</returns>
    Task Log(LogMessage log);

    /// <summary>
    /// Logs a message.
    /// </summary>
    /// <param name="severity">The severity level of the log.</param>
    /// <param name="source">The source of the log message (e.g., a component or module name).</param>
    /// <param name="message">The message content to log.</param>
    /// <param name="exception">An optional exception associated with the log entry.</param>
    /// <returns>A task that represents the asynchronous logging operation.</returns>
    Task Log(LogSeverity severity, string source, string message, Exception? exception = null);
}
