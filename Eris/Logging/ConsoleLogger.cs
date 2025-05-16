using Discord;

namespace Eris.Logging;

/// <summary>
/// A simple logger that writes log messages to the console with colored severity levels.
/// </summary>
public class ConsoleLogger : LoggerBase
{
    /// <summary>
    /// Formats a <see cref="LogMessage"/> into a readable string for console output.
    /// </summary>
    /// <param name="logMessage">The log message to format.</param>
    /// <returns>A formatted string representing the log message.</returns>
    private static string FormatLogMessage(LogMessage logMessage)
    {
        string exceptionMessage = logMessage.Exception is null ? string.Empty :
            $"{Environment.NewLine}{logMessage.Exception}";

        return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{logMessage.Severity}] [{logMessage.Source}]: {logMessage.Message}{exceptionMessage}";
    }

    /// <summary>
    /// Returns the appropriate console color based on the log severity.
    /// </summary>
    /// <param name="severity">The severity of the log message.</param>
    /// <returns>The <see cref="ConsoleColor"/> to use.</returns>
    private static ConsoleColor GetConsoleColor(LogSeverity severity)
    {
        return severity switch
        {
            LogSeverity.Critical => ConsoleColor.Red,
            LogSeverity.Error => ConsoleColor.DarkRed,
            LogSeverity.Warning => ConsoleColor.Yellow,
            LogSeverity.Info => ConsoleColor.White,
            LogSeverity.Verbose => ConsoleColor.Gray,
            LogSeverity.Debug => ConsoleColor.Cyan,
            _ => ConsoleColor.White
        };
    }

    /// <inheritdoc/>
    public override Task Log(LogMessage logMessage)
    {
        string formattedMessage = FormatLogMessage(logMessage);

        ConsoleColor originalColor = Console.ForegroundColor;
        Console.ForegroundColor = GetConsoleColor(logMessage.Severity);

        Console.WriteLine(formattedMessage);

        Console.ForegroundColor = originalColor;
        return Task.CompletedTask;
    }
}
