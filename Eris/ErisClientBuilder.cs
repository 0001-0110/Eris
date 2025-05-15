using Eris.Handlers.CommandHandlers;
using Eris.Handlers.Messages;
using Eris.Handlers.Services;
using Eris.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Eris;

public class ErisClientBuilder
{
    private readonly IServiceCollection _services;

    public ErisClientBuilder()
    {
        _services = new ServiceCollection();
    }

    public ErisClientBuilder AddLogger<TLogger>() where TLogger : class, ILogger
    {
        _services.AddSingleton<ILogger, TLogger>();
        return this;
    }

    public ErisClientBuilder AddCommandHandler<TCommandHanlder>() where TCommandHanlder : CommandHandler
    {
        _services.AddSingleton<CommandHandler, TCommandHanlder>();
        return this;
    }

    public ErisClientBuilder AddMessageHandler<TMessageHandler>() where TMessageHandler : class, IMessageHandler
    {
        _services.AddSingleton<IMessageHandler, TMessageHandler>();
        return this;
    }

    public ErisClientBuilder AddServiceHandler<TServiceHandler>() where TServiceHandler : class, IServiceHandler
    {
        _services.AddSingleton<IServiceHandler, TServiceHandler>();
        return this;
    }

    public ErisClient Build()
    {
        return new ErisClient(_services);
    }
}
