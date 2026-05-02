using Navigator.Extensions.AI.Models;
using Telegram.Bot.Types;

namespace Navigator.Extensions.AI.Services;

/// <summary>
///     Stores and retrieves AI chat context for Telegram conversations.
/// </summary>
public interface IChatContextStore
{
    /// <summary>
    ///     Appends an incoming Telegram message to the stored chat context.
    /// </summary>
    /// <param name="message">The incoming message.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task AppendIncomingMessageAsync(Message message, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Gets the chat context associated with the specified update.
    /// </summary>
    /// <param name="update">The update whose chat context should be loaded.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The chat context for the update.</returns>
    Task<ChatContext> GetForUpdateAsync(Update update, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Creates a new empty chat context.
    /// </summary>
    /// <returns>An empty chat context.</returns>
    ChatContext CreateEmptyContext();
}
