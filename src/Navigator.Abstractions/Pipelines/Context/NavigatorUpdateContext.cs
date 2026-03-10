using Telegram.Bot.Types;

namespace Navigator.Abstractions.Pipelines.Context;

/// <summary>
///     The context of a Navigator update.
/// </summary>
public class NavigatorUpdateContext
{
    /// <summary>
    ///     The Telegram update.
    /// </summary>
    public readonly Update Update;

    /// <summary>
    ///     Initializes a new instance of the <see cref="NavigatorUpdateContext"/> class.
    /// </summary>
    /// <param name="update"></param>
    public NavigatorUpdateContext(Update update)
    {
        Update = update;
    }
}