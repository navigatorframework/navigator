namespace Navigator.Extensions.Store.Entities;

public class User
{
    public User()
    {
        ProviderEntities = new List<INavigatorProviderUserEntity>();
        Chats = new List<Chat>();
    }
    public Guid Id { get; set; }
    
    public IList<INavigatorProviderUserEntity> ProviderEntities { get; set; }

    /// <summary>
    /// Chats related to the user.
    /// </summary>
    public IList<Chat> Chats { get; set; }
    
    public ICollection<Conversation> Conversations { get; set; }

    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}

public interface INavigatorProviderUserEntity
{
    public Guid Id { get; set; }
}