using Navigator.Abstractions.Telegram;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Strategies.Queued.Telegram;

/// <summary>
///     Extension methods for deriving queue partition keys from Telegram updates.
/// </summary>
public static class TelegramUpdateQueueExtensions
{
    /// <summary>
    ///     Derives a deterministic partition key for queue-based update processing.
    ///     Updates that share the same key are processed sequentially; different keys
    ///     may be processed in parallel. Always returns a non-null value.
    /// </summary>
    /// <param name="update">The Telegram update to derive a queue key from.</param>
    /// <returns>
    ///     A prefixed string key such as <c>chat:123</c>, <c>user:42</c>,
    ///     <c>poll:abc</c>, or <c>default_queue</c> as a last resort.
    /// </returns>
    public static string GetQueueKey(this Update update)
    {
        var chat = update.GetChatOrDefault();
        if (chat is not null)
            return $"chat:{chat.Id}";

        if (update.Type == UpdateType.BusinessConnection && update.BusinessConnection is { } bc)
            return $"user:{bc.UserChatId}";

        if (update.Type == UpdateType.CallbackQuery && update.CallbackQuery is { ChatInstance: { } chatInstance })
            return $"callback-instance:{chatInstance}";

        var user = update.GetUserOrDefault();
        if (user is not null)
            return $"user:{user.Id}";

        if (update.Type == UpdateType.Poll && update.Poll is { } poll)
            return $"poll:{poll.Id}";

        if (update.Type == UpdateType.PollAnswer && update.PollAnswer is { } pollAnswer)
            return $"poll:{pollAnswer.PollId}";

        return "default_queue";
    }
}
