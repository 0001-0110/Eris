using Discord;
using Eris.Handlers.CommandHandlers.Request;

namespace Eris.Handlers.CommandHandlers.Commands;

public abstract class CommandGroup : ICommand
{
    private readonly IDictionary<string, SubCommandHandler> _handlers;

    protected abstract IEnumerable<SubCommandHandler> SubCommandHandlers { get; }

    public ApplicationCommandOptionType Type => ApplicationCommandOptionType.SubCommandGroup;

    public CommandGroup()
    {
        _handlers = SubCommandHandlers.ToDictionary(handler => handler.Name);
    }

    public SlashCommandOptionBuilder[] CreateOptions()
    {
        return _handlers.Values.Select(handler => handler.CreateCommand()).ToArray();
    }

    public Task Execute(ICommandRequest request)
    {
        return _handlers[request.CommandName].Execute(request.GetSubCommand());
    }
}
