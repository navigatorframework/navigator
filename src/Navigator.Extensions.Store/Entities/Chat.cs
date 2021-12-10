using Navigator.Extensions.Store.Entities;

public class Chat
{
    public Chat()
    {
        ProviderEntities = new List<INavigatorProviderChatEntity>();
        Users = new List<User>();
    }

    public Guid Id { get; set; }
    
    public IList<INavigatorProviderChatEntity> ProviderEntities { get; set; }

    /// <summary>
    /// Users related to the chat.
    /// </summary>
    public ICollection<User> Users { get; set; }
    
    public ICollection<Conversation> Conversations { get; set; }

    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}

public interface INavigatorProviderChatEntity
{
    public Guid Id { get; set; }
}