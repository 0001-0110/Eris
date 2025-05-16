namespace Eris.Handlers.Services;

/// <summary>
/// Manages the lifecycle of background services used by the bot.
/// </summary>
internal interface IServiceManager
{
    /// <summary>
    /// Starts all registered services with the provided cancellation token.
    /// </summary>
    /// <param name="cancellationToken">Token used to signal cancellation of services.</param>
    Task StartServices(CancellationToken cancellationToken);
}
