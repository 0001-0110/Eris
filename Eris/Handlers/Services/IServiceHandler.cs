namespace Eris.Handlers.Services;

/// <summary>
/// Represents a long-running background service that can be managed by the <see cref="IServiceManager"/>.
/// </summary>
public interface IServiceHandler
{
    /// <summary>
    /// Starts the service's execution logic. This method should run until the <paramref name="cancellationToken"/> is triggered.
    /// </summary>
    /// <param name="cancellationToken">Token used to request graceful cancellation of the service.</param>
    Task Run(CancellationToken cancellationToken);
}
