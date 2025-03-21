using Discord;

namespace Eris.Handlers.CommandHandlers;

public abstract class SubCommandHandler : CommandHandler
{
    internal virtual SlashCommandOptionBuilder CreateCommand()
    {
        return new SlashCommandOptionBuilder()
            .WithName(Name)
            .WithDescription(Description)
            .WithType(Command.Type)
            .AddOptions(CreateOptions());
    }
}
