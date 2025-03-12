using Eris.Handlers.Commands.Request;

namespace Eris.Handlers.CommandHandlers.Commands;

public interface ICommand
{
    Task Execute(ICommandRequest request);
}
