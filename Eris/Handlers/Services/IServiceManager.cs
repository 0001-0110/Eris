namespace Eris.Handlers.Services;

internal interface IServiceManager : IHandlerManager<IServiceHandler>
{
    Task StartServices(CancellationToken cancellationToken);
}
