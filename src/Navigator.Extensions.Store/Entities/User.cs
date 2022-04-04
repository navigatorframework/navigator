using Navigator.Entities;

namespace Navigator.Extensions.Store.Entities;

public class User : Navigator.Entities.User
{
    public User()
    {
        Chats = new List<Chat>();
        Conversations = new List<Conversation>();
        Data = new Dictionary<string, string>();
        FirstInteractionAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Chats related to the user.
    /// </summary>
    public IList<Chat> Chats { get; set; }
    
    public ICollection<Conversation> Conversations { get; set; }
    
    public IDictionary<string, string> Data { get; set; }

    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}