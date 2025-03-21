using Eris.Handlers.CommandHandlers.Commands;

namespace Eris.Handlers.CommandHandlers.BuiltIn;

public class HelpCommandHandler : GlobalCommandHandler
{
    public override string Name => "help";

    protected override ICommand Command => throw new NotImplementedException();
}
