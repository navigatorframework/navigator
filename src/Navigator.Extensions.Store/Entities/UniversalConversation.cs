using Navigator.Entities;

namespace Navigator.Extensions.Store.Entities;

public class UniversalConversation : Navigator.Entities.Conversation
{
    public UniversalConversation()
    {
        FirstInteractionAt = DateTime.UtcNow;
    }
    public UniversalConversation(User user, Chat chat) : base(user, chat)
    {
        FirstInteractionAt = DateTime.UtcNow;
    }

    // public new Guid Id { get; set; }

    public new UniversalChat Chat { get; set; }
    
    public new UniversalUser User { get; set; }

    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}