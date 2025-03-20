using Discord.WebSocket;

namespace Eris.Handlers.CommandHandlers.Request;

public interface ICommandRequest
{
    string CommandName { get; }

    SocketUser Sender { get; }

    ISocketMessageChannel Channel { get; }

    Task Respond(string text);

    SocketSlashCommandDataOption? GetOption(string name);

    ICommandRequest GetSubCommand();
}
