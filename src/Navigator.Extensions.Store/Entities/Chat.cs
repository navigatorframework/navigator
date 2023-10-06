namespace Navigator.Extensions.Store.Entities;

public class Chat : Navigator.Entities.Chat
{
    public Chat()
    {
        Users = new List<User>();
        Conversations = new List<Conversation>();
        Data = new Dictionary<string, string>();
        FirstInteractionAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Users related to the chat.
    /// </summary>
    public ICollection<User> Users { get; set; }
    
    public ICollection<Conversation> Conversations { get; set; }

    public IDictionary<string, string> Data { get; set; }

    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}