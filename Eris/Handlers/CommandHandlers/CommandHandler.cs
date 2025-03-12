using Discord;
using Eris.Handlers.CommandHandlers.Commands;
using Eris.Handlers.Commands.Request;

namespace Eris.Handlers.CommandHandlers;

public abstract class CommandHandler : IHandler
{
    protected abstract ICommand Command { get; }

    public abstract string Name { get; }

    public string Description => (Command as FinalCommand)?.Description ?? string.Empty;

    internal protected abstract SlashCommandOptionBuilder[] CreateOptions();

    public Task Execute(ICommandRequest request)
    {
        return Command.Execute(request);
    }
}
