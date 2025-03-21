using Discord.WebSocket;

namespace Eris.Handlers.CommandHandlers.Request;

public class CommandRequest : ICommandRequest
{
    private readonly SocketSlashCommand _command;
    private readonly SocketSlashCommandDataOption? _subCommand;

    public string CommandName => _subCommand?.Name ?? _command.CommandName;

    public SocketUser Sender => _command.User;

    public ISocketMessageChannel Channel => _command.Channel;

    internal CommandRequest(SocketSlashCommand command)
    {
        _command = command;
    }

    private CommandRequest(SocketSlashCommand request, SocketSlashCommandDataOption subCommand) : this(request)
    {
        _subCommand = subCommand;
    }

    // TODO Change this to use my option wrapper
    public SocketSlashCommandDataOption? GetOption(string name)
    {
        IReadOnlyCollection<SocketSlashCommandDataOption> options = _subCommand?.Options ?? _command.Data.Options;
        return options.FirstOrDefault(option => option.Name == name);
    }

    public ICommandRequest GetSubCommand()
    {
        return new CommandRequest(_command, _subCommand?.Options.First() ?? _command.Data.Options.First());
    }

    public Task Respond(string text)
    {
        // TODO Add more ways to respond later
        return _command.RespondAsync(text);
    }
}
