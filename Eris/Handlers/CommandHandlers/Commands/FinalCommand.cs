using Eris.Handlers.Commands.Request;

namespace Eris.Handlers.CommandHandlers.Commands;

public abstract class FinalCommand : ICommand
{
    public abstract string Description { get; }

    public FinalCommand()
    {
        throw new NotImplementedException();
    }

    public abstract Task Execute(ICommandRequest request);
}
