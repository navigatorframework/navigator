using Navigator.Entities;

namespace Navigator.Extensions.Store.Entities;

public class UniversalUser : Navigator.Entities.User
{
    public UniversalUser()
    {
        Profiles = new List<UserProfile>();
        Chats = new List<UniversalChat>();
        Conversations = new List<UniversalConversation>();
        FirstInteractionAt = DateTime.UtcNow;
    }
    
    // public new Guid Id { get; set; }
    
    public IList<UserProfile> Profiles { get; set; }

    /// <summary>
    /// Chats related to the user.
    /// </summary>
    public IList<UniversalChat> Chats { get; set; }
    
    public ICollection<UniversalConversation> Conversations { get; set; }

    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}