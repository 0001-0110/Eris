using Discord;
using Eris.Logging;

namespace Eris.Handlers.BackgroundTasks;

/// <summary>
/// Responsible for managing the lifecycle and supervision of background tasks.
/// Ensures tasks are restarted with exponential backoff on failure.
/// </summary>
internal class BackgroundTaskManager : IBackgroundTaskManager
{
    private readonly ILogger _logger;
    private readonly IDictionary<IBackgroundTaskHandler, Task> _serviceHandlers;

    public BackgroundTaskManager(ILogger logger, IEnumerable<IBackgroundTaskHandler> serviceHandlers)
    {
        _logger = logger;
        _serviceHandlers = serviceHandlers.ToDictionary(handler => handler, _ => Task.CompletedTask);
    }

    /// <summary>
    /// Continuously runs the provided task. If it exits unexpectedly, it is restarted with exponential backoff.
    /// </summary>
    private async Task RunBackgroundTask(IBackgroundTaskHandler serviceHandler, CancellationToken cancellationToken)
    {
        int failCount = 0;

        while (!cancellationToken.IsCancellationRequested)
        {
            DateTime startTime = DateTime.Now;

            try
            {
                await _logger.Log(LogSeverity.Verbose, nameof(BackgroundTaskManager), $"Service {serviceHandler} is starting");
                await serviceHandler.Run(cancellationToken);
            }
            catch (Exception exception)
            {
                await _logger.Log(LogSeverity.Error, nameof(BackgroundTaskManager),
                    $"Service {serviceHandler} exited with an error:", exception);
            }

            // Reset failure count if the service ran for more than 10 seconds
            TimeSpan uptime = DateTime.Now - startTime;
            if (uptime > TimeSpan.FromSeconds(10))
                failCount = 0;
            else
                failCount++;

            // Calculate exponential backoff delay
            TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, failCount));
            await _logger.Log(LogSeverity.Warning, nameof(BackgroundTaskManager),
                $"Service {serviceHandler} exited, restarting in {delay}");

            await Task.Delay(delay, cancellationToken);
        }
    }

    /// <inheritdoc/>
    public Task Start(CancellationToken cancellationToken)
    {
        foreach (IBackgroundTaskHandler serviceHandler in _serviceHandlers.Keys)
            _serviceHandlers[serviceHandler] = RunBackgroundTask(serviceHandler, cancellationToken);

        return Task.WhenAll(_serviceHandlers.Values);
    }
}
