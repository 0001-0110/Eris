using Discord;

namespace Eris.Handlers.CommandHandlers;

public abstract class BaseCommandHandler : CommandHandler
{
    protected internal override SlashCommandOptionBuilder[] CreateOptions()
    {
        throw new NotImplementedException();
    }

    internal virtual SlashCommandBuilder CreateCommand()
    {
        return new SlashCommandBuilder()
            .WithName(Name)
            .WithDescription(Description)
            .AddOptions(CreateOptions());
    }
}
