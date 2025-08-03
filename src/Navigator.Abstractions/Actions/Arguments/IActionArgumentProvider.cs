using Telegram.Bot.Types;

namespace Navigator.Abstractions.Actions.Arguments;

/// <summary>
///     Interface for providing arguments for the condition and handler delegates of a <see cref="BotAction" />.
/// </summary>
public interface IActionArgumentProvider
{
    /// <summary>
    ///     Returns the arguments for the condition delegate.
    /// </summary>
    /// <param name="update">The <see cref="Update" /> object.</param>
    /// <param name="action">The <see cref="BotAction" />.</param>
    /// <returns></returns>
    public ValueTask<object?[]> GetConditionArguments(Update update, BotAction action);
    
    /// <summary>
    ///     Returns the arguments for the handler delegate.
    /// </summary>
    /// <param name="update">The <see cref="Update" /> object.</param>
    /// <param name="action">The <see cref="BotAction" />.</param>
    /// <returns></returns>
    public ValueTask<object?[]> GetHandlerArguments(Update update, BotAction action);
}