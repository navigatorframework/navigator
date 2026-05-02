namespace Navigator.Extensions.AI.Models;

/// <summary>
///     Represents the rolling conversation history used by AI features.
/// </summary>
public class ChatContext : SlidingBuffer<ChatContextMessage>
{
    /// <summary>
    ///     Initializes a new empty chat context.
    /// </summary>
    /// <param name="maxLength">The maximum number of messages to retain.</param>
    public ChatContext(int maxLength)
        : base(maxLength)
    {
    }

    /// <summary>
    ///     Initializes a new chat context with preloaded messages.
    /// </summary>
    /// <param name="maxLength">The maximum number of messages to retain.</param>
    /// <param name="messages">The messages to preload.</param>
    public ChatContext(int maxLength, IEnumerable<ChatContextMessage> messages)
        : this(maxLength)
    {
        AddRange(messages);
    }
}
