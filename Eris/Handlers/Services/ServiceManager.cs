using Discord;
using Eris.Logging;

namespace Eris.Handlers.Services;

internal class ServiceManager : IServiceManager
{
    private readonly ILogger _logger;
    private readonly IDictionary<IServiceHandler, Task> _serviceHandlers;

    public ServiceManager(ILogger logger, IEnumerable<IServiceHandler> serviceHandlers)
    {
        _logger = logger;
        _serviceHandlers = serviceHandlers.ToDictionary(handler => handler, _ => Task.CompletedTask);
    }

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
            TimeSpan uptime = DateTime.Now - startTime;

            failCount++;
            Console.WriteLine(uptime);
            if (uptime > TimeSpan.FromSeconds(10))
                failCount = 0;

            TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, failCount));
            await _logger.Log(LogSeverity.Warning, nameof(ServiceManager),
                $"Service {serviceHandler} exited, restarting in {delay}");
            await Task.Delay(delay, cancellationToken);
        }
    }

    public Task StartServices(CancellationToken cancellationToken)
    {
        foreach (IServiceHandler serviceHandler in _serviceHandlers.Keys)
            _serviceHandlers[serviceHandler] = RunService(serviceHandler, cancellationToken);

        return Task.WhenAll(_serviceHandlers.Values);
    }
}
