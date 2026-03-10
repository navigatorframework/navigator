using Navigator.Abstractions.Telegram;
using Telegram.Bot.Types;

namespace Navigator.Abstractions.Pipelines.Context;

/// <summary>
///     The context of a Navigator update.
/// </summary>
public class NavigatorUpdateContext
{
    /// <summary>
    ///     The original Telegram update that triggered this context.
    /// </summary>
    public readonly Update Update;

    /// <summary>
    ///     The timestamp when the update was received by the bot.
    /// </summary>
    public readonly DateTime ReceivedAt;

    /// <summary>
    ///     The timestamp when the update was originally sent, if available.
    /// </summary>
    public readonly DateTime? SentAt;

    /// <summary>
    ///     Initializes a new instance of the <see cref="NavigatorUpdateContext"/> class.
    /// </summary>
    /// <param name="update"></param>
    public NavigatorUpdateContext(Update update)
    {
        Update = update;
        ReceivedAt = DateTime.UtcNow;
        SentAt = update.GetDateOrDefault();
    }
}