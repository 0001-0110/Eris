using Discord;
using Discord.Interactions;

namespace Eris.Handlers.CommandHandlers.BuiltIn;

public class HelloCommandHandler : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("hello", "Say hello to your new friend")]
    public Task Hello(IMentionable? user = null)
    {
        return RespondAsync($"Hello {user}");
    }
}
