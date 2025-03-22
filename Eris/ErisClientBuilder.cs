using Eris.Handlers.CommandHandlers;
using Eris.Handlers.CommandHandlers.Manager;
using Eris.Handlers.Messages;
using Eris.Handlers.Services;
using Eris.Logging;
using InjectoPatronum;

namespace Eris;

public class ErisClientBuilder
{
    private readonly LoggerManager _loggerManager;
    private readonly ICommandManager _commandManager;
    private readonly IMessageManager _messageManager;
    private readonly IServiceManager _serviceManager;

    public ErisClientBuilder(IDependencyInjector injector)
    {
        _loggerManager = injector.Instantiate<LoggerManager>();
        injector.MapSingleton<ILogger>(_loggerManager);
        _commandManager = injector.Instantiate<CommandManager>();
        _messageManager = injector.Instantiate<MessageManager>();
        _serviceManager = injector.Instantiate<ServiceManager>();
    }

    public ErisClientBuilder AddLogger(ILogger logger)
    {
        _loggerManager.AddLogger(logger);
        return this;
    }

    public ErisClientBuilder AddCommandHandler<TCommandHandler>() where TCommandHandler : BaseCommandHandler
    {
        _commandManager.AddHandler<TCommandHandler>();
        return this;
    }

    public ErisClientBuilder AddMessageHandler<TMessageHandler>() where TMessageHandler : IMessageHandler
    {
        _messageManager.AddHandler<TMessageHandler>();
        return this;
    }

    public ErisClientBuilder AddService<TServiceHandler>() where TServiceHandler : IServiceHandler
    {
        _serviceManager.AddHandler<TServiceHandler>();
        return this;
    }

    public ErisClient Build()
    {
        return new ErisClient(_loggerManager, _commandManager, _messageManager, _serviceManager);
    }
}
