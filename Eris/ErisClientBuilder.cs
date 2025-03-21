using Eris.Handlers.CommandHandlers;
using Eris.Handlers.CommandHandlers.Manager;
using Eris.Handlers.Messages;
using Eris.Handlers.Services;
using InjectoPatronum;

namespace Eris;

public class ErisClientBuilder
{
    private readonly ICommandManager _commandManager;
    private readonly IMessageManager _messageManager;
    private readonly IServiceManager _serviceManager;

    public ErisClientBuilder(IDependencyInjector injector)
    {
        _commandManager = injector.Instantiate<CommandManager>();
        _messageManager = injector.Instantiate<MessageManager>();
        _serviceManager = injector.Instantiate<ServiceManager>();
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
        return new ErisClient(_commandManager, _messageManager, _serviceManager);
    }
}
