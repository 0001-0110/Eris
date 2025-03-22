using Discord;
using Eris.Handlers.CommandHandlers.Request;

namespace Eris.Handlers.CommandHandlers.Commands;

public abstract class FinalCommand : ICommand
{
    public abstract string Description { get; }

    public abstract IEnumerable<CommandOption> Options { get; }

    public ApplicationCommandOptionType Type => ApplicationCommandOptionType.SubCommand;

    public SlashCommandOptionBuilder[] CreateOptions()
    {
        return Options.Select(option => option.CreateOption()).ToArray();
    }

    public abstract Task Execute(ICommandRequest request);
}
