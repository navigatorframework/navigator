namespace Navigator.Extensions.Store.Entities;

public class UniversalChat : Navigator.Entities.Chat
{
    public UniversalChat()
    {
        Users = new List<UniversalUser>();
        Conversations = new List<UniversalConversation>();
        FirstInteractionAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Users related to the chat.
    /// </summary>
    public ICollection<UniversalUser> Users { get; set; }
    
    public ICollection<UniversalConversation> Conversations { get; set; }

    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}