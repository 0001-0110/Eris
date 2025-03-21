using Discord;
using Eris.Handlers.CommandHandlers.Request;

namespace Eris.Handlers.CommandHandlers.Commands;

public abstract class FinalCommand : ICommand
{
    public abstract string Description { get; }

    public ApplicationCommandOptionType Type => ApplicationCommandOptionType.SubCommand;

    public SlashCommandOptionBuilder[] CreateOptions()
    {
        // TODO Handle command options when implemented
        return [];
    }

    public abstract Task Execute(ICommandRequest request);
}
