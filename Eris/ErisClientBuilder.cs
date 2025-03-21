using Eris.Handlers.Commands;
using Eris.Handlers.Messages;
using Eris.Handlers.Services;

namespace Eris;

public class ErisClientBuilder
{
    private readonly ICommandManager _commandManager;
    private readonly IMessageManager _messageManager;
    private readonly IServiceManager _serviceManager;

    public ErisClientBuilder()
    {
        _commandManager = new CommandManager();
        _messageManager = new MessageManager();
        _serviceManager = new ServiceManager();
    }

    public ErisClientBuilder AddCommandHandler(ICommandHandler commandHandler)
    {
        _commandManager.AddHandler(commandHandler);
        return this;
    }

    public ErisClientBuilder AddMessageHandler(IMessageHandler messageHandler)
    {
        _messageManager.AddHandler(messageHandler);
        return this;
    }

    public ErisClientBuilder AddService(IServiceHandler service)
    {
        _serviceManager.AddHandler(service);
        return this;
    }

    public ErisClient Build()
    {
        return new ErisClient(_commandManager, _messageManager, _serviceManager);
    }
}
