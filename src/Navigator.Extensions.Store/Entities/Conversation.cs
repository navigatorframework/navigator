using Navigator.Entities;

namespace Navigator.Extensions.Store.Entities;

public class Conversation : Navigator.Entities.Conversation
{
    public Conversation()
    {
        Data = new Dictionary<string, string>();
        FirstInteractionAt = DateTime.UtcNow;
    }
    public Conversation(User user, Chat chat) : base(user, chat)
    {
        Data = new Dictionary<string, string>();
        FirstInteractionAt = DateTime.UtcNow;
    }

    // public new Guid Id { get; set; }

    public new Chat Chat { get; set; }
    
    public new User User { get; set; }
    
    public IDictionary<string, string> Data { get; set; }

    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}