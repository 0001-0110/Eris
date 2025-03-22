using Discord;
using Eris.Handlers.CommandHandlers.Request;

namespace Eris.Handlers.CommandHandlers;

public abstract class CommandOption
{
    protected readonly string _name;
    protected readonly string _description;
    protected readonly ApplicationCommandOptionType _type;
    private readonly bool _isRequired;

    protected CommandOption(string name, string description, ApplicationCommandOptionType type, bool isRequired)
    {
        _name = name;
        _description = description;
        _type = type;
        _isRequired = isRequired;
    }

    public SlashCommandOptionBuilder CreateOption()
    {
        return new SlashCommandOptionBuilder()
            .WithName(_name)
            .WithDescription(_description)
            .WithType(_type)
            .WithRequired(_isRequired);
    }
}

public class CommandOption<T> : CommandOption
{
    private static ApplicationCommandOptionType GetOptionType()
    {
        if (typeof(T) == typeof(string))
            return ApplicationCommandOptionType.String;

        if (typeof(T) == typeof(long))
            return ApplicationCommandOptionType.Integer;

        if (typeof(T) == typeof(bool))
            return ApplicationCommandOptionType.Boolean;

        if (typeof(T) == typeof(IUser))
            return ApplicationCommandOptionType.User;

        if (typeof(T) == typeof(IChannel))
            return ApplicationCommandOptionType.Channel;

        if (typeof(T) == typeof(IRole))
            return ApplicationCommandOptionType.Role;

        if (typeof(T) == typeof(IMentionable))
            return ApplicationCommandOptionType.Mentionable;

        if (typeof(T) == typeof(double))
            return ApplicationCommandOptionType.Number;

        if (typeof(T) == typeof(IAttachment))
            return ApplicationCommandOptionType.Attachment;

        // TODO
        throw new Exception();
    }

    public CommandOption(string name, string description, bool isRequired = false)
        : base(name, description, GetOptionType(), isRequired) { }

    public T? GetValue(ICommandRequest command)
    {
        return (T?)command.GetOption(_name)?.Value;
    }
}
