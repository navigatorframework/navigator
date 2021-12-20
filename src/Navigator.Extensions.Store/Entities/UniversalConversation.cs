namespace Navigator.Extensions.Store.Entities;

public class UniversalConversation : Navigator.Entities.Conversation
{
    public new UniversalChat Chat { get; set; }
    
    public new UniversalUser User { get; set; }
    
    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}