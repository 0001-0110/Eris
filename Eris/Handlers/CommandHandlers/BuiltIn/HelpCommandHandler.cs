using Discord.Interactions;

namespace Eris.Handlers.CommandHandlers.BuiltIn;

public class HelpCommandHandler : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("help", "")]
    public Task Help()
    {
        return RespondAsync("Not implemented yet");
    }
}
