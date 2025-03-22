using Discord;
using Eris.Logging;
using InjectoPatronum;

namespace Eris.Handlers.Services;

internal class ServiceManager : IServiceManager
{
    private readonly IDependencyInjector _injector;
    private readonly ILogger _logger;
    private readonly IDictionary<Type, Task> _services;

    public ServiceManager(IDependencyInjector injector, ILogger logger)
    {
        _injector = injector;
        _logger = logger;
        _services = new Dictionary<Type, Task>();
    }

    private async Task RunService(Type serviceHandler, CancellationToken cancellationToken)
    {
        int failCount = 0;
        while (!cancellationToken.IsCancellationRequested)
        {
            DateTime startTime = DateTime.Now;
            try
            {
                IServiceHandler handler = (IServiceHandler)_injector.Instantiate(serviceHandler);
                await _logger.Log(LogSeverity.Verbose, nameof(ServiceManager),
                    $"Service {serviceHandler.Name} is starting");
                await handler.Run(cancellationToken);
            }
            catch (Exception exception)
            {
                await _logger.Log(LogSeverity.Error, nameof(ServiceManager),
                    $"Service {serviceHandler.Name} exited with an error:", exception);
            }
            TimeSpan uptime = DateTime.Now - startTime;

            failCount++;
            Console.WriteLine(uptime);
            if (uptime > TimeSpan.FromSeconds(10))
                failCount = 0;

            TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, failCount));
            await _logger.Log(LogSeverity.Warning, nameof(ServiceManager),
                $"Service {serviceHandler.Name} exited, restarting in {delay}");
            await Task.Delay(delay, cancellationToken);
        }
    }

    public void AddHandler<TServiceHandler>() where TServiceHandler : IServiceHandler
    {
        _services.Add(typeof(TServiceHandler), Task.CompletedTask);
    }

    public Task StartServices(CancellationToken cancellationToken)
    {
        foreach (Type service in _services.Keys)
        {
            _services[service] = RunService(service, cancellationToken);
        }

        return Task.WhenAll(_services.Values);
    }
}
