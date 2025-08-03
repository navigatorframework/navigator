using Telegram.Bot.Types;

namespace Navigator.Abstractions.Actions.Arguments;

/// <summary>
///     Defines a contract for resolving method parameters for bot action handlers.
///     Multiple resolvers work together in a chain-of-responsibility pattern to resolve all required parameters.
/// </summary>
public interface IArgumentResolver
{
    /// <summary>
    ///     Attempts to resolve an argument of the specified type for a bot action handler.
    ///     If this resolver cannot provide the requested type, it should return null to allow other resolvers to try.
    /// </summary>
    /// <param name="type">The parameter type that needs to be resolved for the action handler.</param>
    /// <param name="update">The Telegram update that triggered the action execution.</param>
    /// <param name="action">The bot action being executed, containing metadata about the handler and its requirements.</param>
    /// <returns>
    ///     The resolved argument instance if this resolver can handle the specified type, or null if this resolver cannot
    ///     provide the requested argument type.
    /// </returns>
    public ValueTask<object?> GetArgument(Type type, Update update, BotAction action);
}