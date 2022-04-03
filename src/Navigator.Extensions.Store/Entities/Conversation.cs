using Navigator.Entities;

namespace Navigator.Extensions.Store.Entities;

public class Conversation : Navigator.Entities.Conversation
{
    public Conversation()
    {
        FirstInteractionAt = DateTime.UtcNow;
    }
    public Conversation(Navigator.Entities.User user, Chat chat) : base(user, chat)
    {
        FirstInteractionAt = DateTime.UtcNow;
    }

    // public new Guid Id { get; set; }

    public new Chat Chat { get; set; }
    
    public new User User { get; set; }

    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}