using Eris.Handlers.Commands;
using Eris.Handlers.Messages;
using Eris.Handlers.Services;
using Eris.Logging;

namespace Eris;

public class ErisClientBuilder
{
    private readonly LoggerManager _loggerManager;
    private readonly ICommandManager _commandManager;
    private readonly IMessageManager _messageManager;
    private readonly IServiceManager _serviceManager;

    public ErisClientBuilder()
    {
        _loggerManager = new LoggerManager();
        _commandManager = new CommandManager();
        _messageManager = new MessageManager();
        _serviceManager = new ServiceManager();
    }

    public ErisClientBuilder AddLogger(ILogger logger)
    {
        _loggerManager.AddLogger(logger);
        return this;
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
        return new ErisClient(_loggerManager, _commandManager, _messageManager, _serviceManager);
    }
}
