using Eris.Handlers.CommandHandlers;
using Eris.Handlers.CommandHandlers.Commands;
using Eris.Handlers.Commands.Request;

namespace Eris;

internal class HelloCommandHandler : GlobalCommandHandler
{
    private class HelloCommand : FinalCommand
    {
        public override string Description => "Say hello to your new friend!";

        public override Task Execute(ICommandRequest request)
        {
            throw new NotImplementedException();
        }
    }

    protected override ICommand Command => new HelloCommand();

    public override string Name => "Test";
}
