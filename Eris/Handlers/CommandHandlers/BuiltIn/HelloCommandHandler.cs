using Discord;
using Discord.Interactions;

namespace Eris.Handlers.CommandHandlers.BuiltIn;

/// <summary>
/// Provides a slash command to greet a mentioned user.
/// </summary>
public class HelloCommandHandler : CommandHandler
{
    /// <summary>
    /// Handles the /hello slash command.
    /// Responds with a greeting mentioning the specified user if provided.
    /// </summary>
    /// <param name="user">The user to greet. If null, no specific user is mentioned.</param>
    [SlashCommand("hello", "Say hello to your new friend")]
    public Task Hello(IMentionable? user = null)
    {
        return RespondAsync($"Hello {user?.Mention ?? "world!"}");
    }
}
