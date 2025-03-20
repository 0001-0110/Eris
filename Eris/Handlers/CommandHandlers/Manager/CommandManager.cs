using Discord.WebSocket;
using Eris.Handlers.CommandHandlers.Request;
using InjectoPatronum;

namespace Eris.Handlers.CommandHandlers.Manager;

internal class CommandManager : ICommandManager
{
    private readonly IDependencyInjector _injector;
    private readonly IDictionary<string, Type> _handlers;

    public CommandManager(IDependencyInjector injector)
    {
        _injector = injector;
        _handlers = new Dictionary<string, Type>();
    }

    public void AddHandler<TCommandHandler>() where TCommandHandler : BaseCommandHandler
    {
        _handlers.Add(_injector.Instantiate<TCommandHandler>().Name, typeof(TCommandHandler));
    }

    public async Task CreateCommands(DiscordSocketClient client)
    {
        await client.BulkOverwriteGlobalApplicationCommandsAsync(_handlers.Values
            .Where(handlerType => handlerType.IsAssignableTo(typeof(GlobalCommandHandler)))
            .Select(type => (GlobalCommandHandler)_injector.Instantiate(type))
            .Select(handler => handler.CreateCommand().Build()).ToArray());

        foreach (SocketGuild guild in client.Guilds)
        {
            await guild.BulkOverwriteApplicationCommandAsync(_handlers.Values
                .Where(handlerType => handlerType.IsAssignableTo(typeof(GuildCommandHandler)))
                .Select(type => (GuildCommandHandler)_injector.Instantiate(type))
                .Where(handler => handler.IsEnabled(guild))
                .Select(handler => handler.CreateCommand().Build()).ToArray());
        }
    }

    public Task HandleCommand(SocketSlashCommand command)
    {
        // TODO Temp debug, switch to proper logging with DI
        try
        {
            return ((CommandHandler)_injector.Instantiate(_handlers[command.CommandName]))
                .Execute(new CommandRequest(command));
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
            throw;
        }
    }
}
