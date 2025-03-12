namespace Eris.Handlers.Commands.Request;

public interface ICommandRequest
{
    public string CommandName { get; }

    public ICommandRequest GetSubCommand();
}
