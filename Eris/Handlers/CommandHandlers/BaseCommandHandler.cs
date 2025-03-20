using Discord;

namespace Eris.Handlers.CommandHandlers;

public abstract class BaseCommandHandler : CommandHandler
{
    internal virtual SlashCommandBuilder CreateCommand()
    {
        return new SlashCommandBuilder()
            .WithName(Name)
            .WithDescription(Description)
            .AddOptions(CreateOptions());
    }
}
