namespace Eris.Handlers.Services;

internal class ServiceManager : IServiceManager
{
    public void AddHandler<TServiceHandler>() where TServiceHandler : IServiceHandler
    {
        throw new NotImplementedException();
    }

    public Task StartServices(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
