using Discord;
using Eris.Logging;

namespace Eris.Handlers.Services;

/// <summary>
/// Responsible for managing the lifecycle and supervision of background services.
/// Ensures services are restarted with exponential backoff on failure.
/// </summary>
internal class ServiceManager : IServiceManager
{
    private readonly ILogger _logger;
    private readonly IDictionary<IServiceHandler, Task> _serviceHandlers;

    public ServiceManager(ILogger logger, IEnumerable<IServiceHandler> serviceHandlers)
    {
        _logger = logger;
        _serviceHandlers = serviceHandlers.ToDictionary(handler => handler, _ => Task.CompletedTask);
    }

    /// <summary>
    /// Continuously runs the provided service. If it exits unexpectedly, it is restarted with exponential backoff.
    /// </summary>
    private async Task RunService(IServiceHandler serviceHandler, CancellationToken cancellationToken)
    {
        int failCount = 0;

        while (!cancellationToken.IsCancellationRequested)
        {
            DateTime startTime = DateTime.Now;

            try
            {
                await _logger.Log(LogSeverity.Verbose, nameof(ServiceManager), $"Service {serviceHandler} is starting");
                await serviceHandler.Run(cancellationToken);
            }
            catch (Exception exception)
            {
                await _logger.Log(LogSeverity.Error, nameof(ServiceManager),
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
            await _logger.Log(LogSeverity.Warning, nameof(ServiceManager),
                $"Service {serviceHandler} exited, restarting in {delay}");

            await Task.Delay(delay, cancellationToken);
        }
    }

    /// <inheritdoc/>
    public Task StartServices(CancellationToken cancellationToken)
    {
        foreach (IServiceHandler serviceHandler in _serviceHandlers.Keys)
            _serviceHandlers[serviceHandler] = RunService(serviceHandler, cancellationToken);

        return Task.WhenAll(_serviceHandlers.Values);
    }
}
