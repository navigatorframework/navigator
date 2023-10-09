namespace Navigator.Extensions.Store.Entities;

/// <summary>
/// Chat.
/// </summary>
public class Chat
{
    /// <summary>
    /// Internal constructor.
    /// </summary>
    protected Chat()
    {
        Users = new List<User>();
        Conversations = new List<Conversation>();
    }

    /// <summary>
    /// Chat constructor from <see cref="Navigator.Entities.Chat"/>
    /// </summary>
    /// <param name="chat"><see cref="Navigator.Entities.Chat"/></param>
    public Chat(Navigator.Entities.Chat chat)
    {
        Id = chat.Id;
        Title = chat.Title;
        Type = chat.Type;
        IsForum = chat.IsForum;

        FirstInteractionAt = DateTime.UtcNow;

        Users = new List<User>();
        Conversations = new List<Conversation>();
    }


    public long Id { get; set; }

    public string? Title { get; set; }
    public Navigator.Entities.Chat.ChatType Type { get; set; }
    public bool? IsForum { get; set; }

    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }

    /// <summary>
    /// Users related to the chat.
    /// </summary>
    public ICollection<User> Users { get; set; }

    /// <summary>
    /// Conversations related to the chat.
    /// </summary>
    public ICollection<Conversation> Conversations { get; set; }
}