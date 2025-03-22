using Discord;

namespace Eris.Logging;

public class ConsoleLogger : LoggerBase
{
    private string FormatLogMessage(LogMessage logMessage)
    {
        string exceptionMessage = logMessage.Exception is null ? string.Empty :
            $"{Environment.NewLine}{logMessage.Exception}";

        return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{logMessage.Severity}] [{logMessage.Source}]: {logMessage.Message}{exceptionMessage}";
    }

    private ConsoleColor GetConsoleColor(LogSeverity severity)
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
