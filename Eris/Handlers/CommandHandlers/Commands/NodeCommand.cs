using Eris.Handlers.Commands.Request;

namespace Eris.Handlers.CommandHandlers.Commands;

public abstract class CommandGroup : ICommand
{
    private readonly IDictionary<string, SubCommandHandler> _handlers;

    public abstract IEnumerable<SubCommandHandler> SubCommandHandlers { get; }

    public CommandGroup(IEnumerable<SubCommandHandler> subCommandHandlers)
    {
        throw new NotImplementedException();
    }

    public Task Execute(ICommandRequest request)
    {
        return _handlers[request.CommandName].Execute(request.GetSubCommand());
    }
}
