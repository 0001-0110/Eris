using Discord;
using Discord.Interactions;
using Discord.Rest;
using Discord.WebSocket;
using Eris.Configuration;
using Eris.Handlers.CommandHandlers;
using Eris.Handlers.CommandHandlers.Manager;
using Eris.Handlers.Messages;
using Eris.Handlers.Services;
using Eris.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Eris;

/// <summary>
/// A builder class for configuring and constructing an instance of <see cref="ErisClient"/>.
/// This allows users to register their own services, commands, message handlers, and logger before building the bot.
/// </summary>
public class ErisClientBuilder
{
    private readonly IServiceCollection _services;

    public ErisClientBuilder()
    {
        _services = new ServiceCollection();
    }

    public ErisClientBuilder WithConfiguration(IConfiguration configuration)
    {
        _services.Configure<DiscordOptions>(configuration);
        return this;
    }

    /// <summary>
    /// Registers a custom logger implementation to be used by the bot.
    /// </summary>
    /// <typeparam name="TLogger">A class implementing <see cref="ILogger"/>.</typeparam>
    /// <returns>The current builder instance for chaining.</returns>
    public ErisClientBuilder AddLogger<TLogger>() where TLogger : class, ILogger
    {
        _services.AddSingleton<ILogger, TLogger>();
        return this;
    }

    /// <summary>
    /// Registers a command handler to be used by the command manager.
    /// Multiple handlers can be added.
    /// </summary>
    /// <typeparam name="TCommandHanlder">A class inheriting from <see cref="CommandHandler"/>.</typeparam>
    /// <returns>The current builder instance for chaining.</returns>
    public ErisClientBuilder AddCommandHandler<TCommandHanlder>() where TCommandHanlder : CommandHandler
    {
        _services.AddSingleton<CommandHandler, TCommandHanlder>();
        return this;
    }

    /// <summary>
    /// Registers a message handler to be used in the message handler chain.
    /// Handlers will be called in the order they are added.
    /// </summary>
    /// <typeparam name="TMessageHandler">A class implementing <see cref="IMessageHandler"/>.</typeparam>
    /// <returns>The current builder instance for chaining.</returns>
    public ErisClientBuilder AddMessageHandler<TMessageHandler>() where TMessageHandler : class, IMessageHandler
    {
        _services.AddSingleton<IMessageHandler, TMessageHandler>();
        return this;
    }

    /// <summary>
    /// Registers a service handler that will run a background task.
    /// Services should only stop when the cancellation token is cancelled.
    /// </summary>
    /// <typeparam name="TServiceHandler">A class implementing <see cref="IServiceHandler"/>.</typeparam>
    /// <returns>The current builder instance for chaining.</returns>
    public ErisClientBuilder AddServiceHandler<TServiceHandler>() where TServiceHandler : class, IServiceHandler
    {
        _services.AddSingleton<IServiceHandler, TServiceHandler>();
        return this;
    }

    /// <summary>
    /// Builds the configured <see cref="ErisClient"/>, resolving all dependencies.
    /// This also configures and registers internal services required for bot operation.
    /// </summary>
    /// <returns>An initialized instance of <see cref="ErisClient"/>.</returns>
    public ErisClient Build()
    {
        DiscordSocketClient client = new DiscordSocketClient(
            // TODO Be more restrictive depending on what is actually used
            new DiscordSocketConfig() { GatewayIntents = GatewayIntents.All });

        IServiceProvider serviceProvider = _services
            .AddSingleton<IRestClientProvider>(client)
            .AddSingleton<DiscordSocketClient>(client)
            .AddSingleton<InteractionService>()
            .AddSingleton<ICommandManager, CommandManager>()
            .AddSingleton<IMessageManager, MessageManager>()
            .AddSingleton<IServiceManager, ServiceManager>()
            .BuildServiceProvider();

        return ActivatorUtilities.CreateInstance<ErisClient>(serviceProvider);
    }
}
