using Navigator.Entities;

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


    /// <summary>
    /// Id of the chat.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Title of the chat.
    /// <remarks>
    ///     Optional.
    /// </remarks>
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// Type of the chat, can be any of <see cref="Navigator.Entities.Chat.ChatType"/>.
    /// </summary>
    public Navigator.Entities.Chat.ChatType Type { get; set; }
    
    /// <summary>
    /// If the supergroup chat is a forum (has topics enabled).
    /// <remarks>
    ///     Optional.
    /// </remarks>
    /// </summary>
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