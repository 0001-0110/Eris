namespace Eris.Handlers.Services;

public interface IServiceHandler : IHandler
{
    Task Run(CancellationToken cancellationToken);
}
