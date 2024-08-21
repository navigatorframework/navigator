using Navigator.Actions;
using Telegram.Bot.Types;

namespace Navigator.Strategy.TypeProvider;

/// <summary>
///     Provides an argument of a given type for a given <see cref="Update" /> and <see cref="BotAction" />.
/// </summary>
public interface IArgumentTypeProvider
{
    /// <summary>
    ///     Gets priority of the provider.
    /// </summary>
    public ushort Priority { get; }

    /// <summary>
    ///     Gets an instance of the argument requested given the supplied <see cref="Update" /> and <see cref="BotAction" />.
    /// </summary>
    /// <param name="type">The type of the argument to get.</param>
    /// <param name="update">The supplied <see cref="Update" />.</param>
    /// <param name="action">The supplied <see cref="BotAction" />.</param>
    /// <returns>An instance of the argument requested.</returns>
    public ValueTask<object?> GetArgument(Type type, Update update, BotAction action);
}