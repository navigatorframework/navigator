namespace Navigator.Extensions.AI.Models;

/// <summary>
/// 
/// </summary>
public class ChatContext : SlidingBuffer<ChatContextMessage>
{
    public ChatContext(int maxLength)
        : base(maxLength)
    {
    }

    public ChatContext(int maxLength, IEnumerable<ChatContextMessage> messages)
        : this(maxLength)
    {
        AddRange(messages);
    }
}
