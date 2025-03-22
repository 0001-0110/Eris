using Discord;

namespace Eris.Logging;

internal class LoggerManager : LoggerBase
{
    private readonly ICollection<ILogger> _loggers;

    public LoggerManager()
    {
        _loggers = [];
    }

    public void AddLogger(ILogger logger)
    {
        _loggers.Add(logger);
    }

    public override Task Log(LogMessage log)
    {
        return Task.WhenAll(_loggers.Select(logger => logger.Log(log)));
    }
}
