using Eris.Handlers.CommandHandlers.Commands;
using Eris.Handlers.CommandHandlers.Request;

namespace Eris.Handlers.CommandHandlers.BuiltIn;

public class HelloCommandHandler : GlobalCommandHandler
{
    private class HelloCommand : FinalCommand
    {
        public override string Description => "Say hello to your new friend!";

        public override Task Execute(ICommandRequest request)
        {
            return request.Respond("Hello, World!");
        }
    }

    protected override ICommand Command => new HelloCommand();

    public override string Name => "hello";
}
