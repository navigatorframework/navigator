using Telegram.Bot.Types;

namespace Navigator.Abstractions.Entities;

/// <summary>
/// Represents a Navigator Chat.
/// </summary>
public record Chat(long Id, Chat.ChatType Type)
{
    /// <summary>
    /// Telegram identifier for the chat.
    /// </summary>
    public long Id { get; init; } = Id;

    /// <summary>
    /// Title of the chat.
    /// <remarks>
    ///     Optional.
    /// </remarks>
    /// </summary>
    public string? Title { get; init; }

    /// <summary>
    /// Type of the chat, can be any of <see cref="ChatType"/>.
    /// </summary>
    public ChatType Type { get; init; } = Type;

    /// <summary>
    /// If the supergroup chat is a forum (has topics enabled).
    /// <remarks>
    ///     Optional.
    /// </remarks>
    /// </summary>
    public bool IsForum { get; init; }

    /// <summary>
    /// Type of Chat.
    /// </summary>
    public enum ChatType
    {
        /// <summary>
        /// Private.
        /// </summary>
        Private = 1,

        /// <summary>
        /// Group.
        /// </summary>
        Group = 2,

        /// <summary>
        /// Channel.
        /// </summary>
        Channel = 3,

        /// <summary>
        /// Supergroup.
        /// </summary>
        Supergroup = 4,

        /// <summary>
        /// Sender.
        /// </summary>
        Sender = 5
    }

    /// <summary>
    ///     Implicitly converts a <see cref="Chat"/> object to a <see cref="ChatId"/> object based on the <see cref="Chat"/>'s Id property.
    ///     This allows for seamless integration between Chat objects and operations that require a <see cref="ChatId"/>.
    /// </summary>
    /// <param name="chat">The <see cref="Chat"/> object to be converted.</param>
    /// <returns><see cref="ChatId"/>
    ///     A new <see cref="ChatId"/> instance initialized with the Id of the provided <see cref="Chat"/> object.
    /// </returns>
    public static implicit operator ChatId(Chat chat)
    {
        return new ChatId(chat.Id);
    }
}