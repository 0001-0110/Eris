using System.Reflection;
using Discord.Interactions;
using Discord.WebSocket;

namespace Eris.Handlers.CommandHandlers.Manager;

internal interface ICommandManager : IHandlerManager<InteractionModuleBase<SocketInteractionContext>>
{
    Task InitCommands(DiscordSocketClient client);
}
