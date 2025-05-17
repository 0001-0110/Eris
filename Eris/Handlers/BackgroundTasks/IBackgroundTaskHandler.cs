namespace Eris.Handlers.BackgroundTasks;

/// <summary>
/// Represents a long-running background task that can be managed by the <see cref="IBackgroundTaskManager"/>.
/// </summary>
public interface IBackgroundTaskHandler
{
    /// <summary>
    /// Starts the task's execution logic. This method should run until the <paramref name="cancellationToken"/> is triggered.
    /// </summary>
    /// <param name="cancellationToken">Token used to request graceful cancellation of the task.</param>
    Task Run(CancellationToken cancellationToken);
}
