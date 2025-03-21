using Discord;
using Eris.Handlers.CommandHandlers.Request;

namespace Eris.Handlers.CommandHandlers.Commands;

public interface ICommand
{
    ApplicationCommandOptionType Type { get; }

    Task Execute(ICommandRequest request);

    SlashCommandOptionBuilder[] CreateOptions();
}
