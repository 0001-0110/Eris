# Eris

**Eris** is a modular, dependency-injection-first Discord client framework built on top of [Discord.Net](https://github.com/discord-net/Discord.Net). It simplifies the process of writing Discord bots.

## ✨ Features

* 🧩 **Modular architecture** with support for message handlers, service handlers, and command handlers.
* 🔗 **Chain-of-responsibility pattern** for message handling.
* 🧠 **Dependency injection** via `Microsoft.Extensions.DependencyInjection`.
* 🛠️ **Builder pattern** for simple and clear configuration.
* 📋 Slash command support via `CommandHandler`s.
* 📡 Automatic service recovery and logging.

---

## 📦 Installation

> Eris is not published yet. Once it is, it will be available via NuGet:

```bash
dotnet add package Eris
```

---

## Configuration
### Setting up your Discord Token

Eris uses the Options pattern to manage configuration, including the Discord bot token.

You need to provide your Discord token via environment variables or configuration files. Eris expects the token to be available under the configuration path:

```json
"Eris": {
  "DiscordToken": "your_token_here"
}
```

Or as an environment variable:
```sh
Eris__DiscordToken=your_token_here
```

### Basic usage
```csharp
using Eris;
using Eris.Logging;
using Eris.Handlers.Messages.BuiltIn;
using Eris.Handlers.CommandHandlers.BuiltIn;

public static async Task Main()
{
    var configuration = new ConfigurationBuilder()
        // Choose either one of these two lines depending on where your config is
        .AddEnvironmentVariables()
        .AddJsonFile("appsettings.json")
        .Build();

    var client = new ErisClientBuilder()
        .WithConfiguration(configuration.GetSection("eris"))
        .AddLogger<ConsoleLogger>()
        .AddMessageHandler<IgnoreBotsMessageHandler>()
        .AddMessageHandler<EchoMessageHandler>()
        .AddCommandHandler<HelloCommandHandler>()
        .AddCommandHandler<VersionCommandHandler>()
        .Build();

    await client.Run();
}
```

---

## 🧱 Components

### 🔧 Command Handlers

Command handlers inherit from `CommandHandler`, which itself extends `InteractionModuleBase<SocketInteractionContext>`. You can define slash commands easily:

```csharp
[SlashCommand("ping", "Responds with pong")]
public Task Ping() => RespondAsync("Pong!");
```

### 💬 Message Handlers

Message handlers implement the `IMessageHandler` interface and follow a **chain of responsibility**. Each handler decides whether it should process a message and whether to stop further processing.

```csharp
public class EchoHandler : IMessageHandler
{
    public async Task<bool> Handle(SocketMessage message)
    {
        await message.Channel.SendMessageAsync(message.Content);
        return true; // Stops further handling
    }
}
```

### 🔄 Service Handlers

Long-running background services implement `IServiceHandler`. Eris will restart services on failure with exponential backoff.

```csharp
public class SampleService : IServiceHandler
{
    public async Task Run(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            // Background logic here
            await Task.Delay(TimeSpan.FromMinutes(1), token);
        }
    }
}
```

---

## 🪵 Logging

Eris defines a simple `ILogger` interface. A basic `ConsoleLogger` is included.

You can create your own logger by implementing the `ILogger` interface or extending `LoggerBase`:
```csharp
public class CustomLogger : LoggerBase
{
    public override Task Log(LogMessage logMessage)
    {
        Console.WriteLine($"[{logMessage.Severity}] {logMessage.Source}: {logMessage.Message}");
        return Task.CompletedTask;
    }
}
```

---

## 🔐 Building the client

Always create your bot instance using `ErisClientBuilder` to avoid breaking initialization.

---

## 📜 License

TODO
