using System.Reflection;
using Discord.Interactions;

namespace Eris.Handlers.CommandHandlers.BuiltIn;

/// <summary>
/// Provides a slash command to display the current application version.
/// </summary>
public class VersionCommandHandler : CommandHandler
{
    /// <summary>
    /// Handles the /version slash command.
    /// Replies with the version number of the running assembly.
    /// </summary>
    [SlashCommand("version", "Get the current version")]
    public Task Version()
    {
        string version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "Version not found";
        return RespondAsync(version);
    }
}
