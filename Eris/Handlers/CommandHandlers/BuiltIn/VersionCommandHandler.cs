using System.Reflection;
using Discord.Interactions;

namespace Eris.Handlers.CommandHandlers.BuiltIn;

public class VersionCommandHandler : CommandHandler
{
    [SlashCommand("version", "Get the current version")]
    public Task Execute()
    {
        string version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "Version not found";
        return RespondAsync(version);
    }
}
