using Telegram.Bot.Types;

namespace Navigator.Extensions.Store.Entities;

/// <summary>
///     Represents a Telegram chat persisted in the store.
/// </summary>
public class Chat
{
    /// <summary>
    ///     EF Core constructor.
    /// </summary>
    protected Chat() { }
    
    /// <summary>
    ///     Creates a new <see cref="Chat"/> with the specified Telegram chat identifier.
    /// </summary>
    /// <param name="externalId">The Telegram chat identifier.</param>
    public Chat(long externalId)
    {
        ExternalId = externalId;
    }
    
    /// <summary>
    ///     Internal unique identifier.
    /// </summary>
    public Guid Id { get; init; } = Guid.CreateVersion7();

    /// <summary>
    ///     The Telegram chat identifier.
    /// </summary>
    public long ExternalId { get; set; }

    /// <summary>
    ///     Conversations that took place in this chat.
    /// </summary>
    public List<Conversation> Conversations { get; set; } = [];
    
    /// <summary>
    ///     Timestamp of the first recorded activity for this chat.
    /// </summary>
    public DateTimeOffset FirstActiveAt { get; set; } = TimeProvider.System.GetUtcNow();
    
    /// <summary>
    ///     Converts a <see cref="Chat"/> to a Telegram <see cref="ChatId"/>.
    /// </summary>
    /// <param name="chat">The chat to convert.</param>
    public static implicit operator ChatId(Chat chat)
    {
        return new ChatId(chat.ExternalId);
    }
}
