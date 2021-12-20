using Navigator.Extensions.Store.Entities;

public class UniversalChat : Navigator.Entities.Chat
{
    public UniversalChat()
    {
        ProviderEntities = new List<INavigatorProviderChatEntity>();
        Users = new List<UniversalUser>();
    }

    public Guid Id { get; set; }
    
    public IList<INavigatorProviderChatEntity> ProviderEntities { get; set; }

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

public interface INavigatorProviderChatEntity
{
    public Guid Id { get; set; }
}