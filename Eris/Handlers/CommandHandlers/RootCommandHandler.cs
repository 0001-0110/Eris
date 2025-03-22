using Discord;

namespace Eris.Handlers.CommandHandlers;

public abstract class RootCommandHandler : CommandHandler
{
    internal virtual SlashCommandBuilder CreateCommand()
    {
        return new SlashCommandBuilder()
            .WithName(Name)
            .WithDescription(Description)
            .AddOptions(CreateOptions());
    }
}
