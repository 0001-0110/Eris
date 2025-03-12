using Discord;

namespace Eris.Handlers.CommandHandlers;

public abstract class SubCommandHandler : CommandHandler
{
    protected internal override SlashCommandOptionBuilder[] CreateOptions()
    {
        throw new NotImplementedException();
    }

    internal virtual SlashCommandOptionBuilder CreateCommand()
    {
        throw new NotImplementedException();
    }
}
