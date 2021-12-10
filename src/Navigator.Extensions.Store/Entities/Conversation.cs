namespace Navigator.Extensions.Store.Entities;

public class Conversation : Navigator.Entities.Conversation
{
    public Chat Chat { get; set; }
    
    public User User { get; set; }
    
    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}