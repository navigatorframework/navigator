using Telegram.Bot.Types;

namespace Navigator.Strategy;

/// <summary>
///     Interface for a strategy implementation for dynamic decision-making based on incoming updates.
/// </summary>
public interface INavigatorStrategy
{
    /// <summary>
    ///     Invokes the strategy for the given <see cref="Update" />.
    /// </summary>
    /// <param name="update">The update that triggered the strategy.</param>
    /// <returns></returns>
    public Task Invoke(Update update);
}