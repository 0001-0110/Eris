using System.Reflection;
using Eris.Handlers.CommandHandlers.Commands;
using Eris.Handlers.CommandHandlers.Request;

namespace Eris.Handlers.CommandHandlers.BuiltIn;

public class VersionCommandHandler : GlobalCommandHandler
{
    private class VersionCommand : FinalCommand
    {
        public override string Description => "Show the current version";

        public override IEnumerable<CommandOption> Options => [];

        public override Task Execute(ICommandRequest request)
        {
            string version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString() ?? "Version not found";
            return request.Respond(version);
        }
    }

    public override string Name => "version";

    protected override ICommand Command => new VersionCommand();
}
