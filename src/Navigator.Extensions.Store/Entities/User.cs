namespace Navigator.Extensions.Store.Entities;

/// <summary>
/// Chat.
/// </summary>
public class User
{
    /// <summary>
    /// Internal constructor.
    /// </summary>
    protected User()
    {
        Chats = new List<Chat>();
        Conversations = new List<Conversation>();
    }

    /// <summary>
    /// User constructor from <see cref="Navigator.Entities.User"/>
    /// </summary>
    /// <param name="user"><see cref="Navigator.Entities.User"/></param>
    public User(Navigator.Entities.User user)
    {
        Id = user.Id;
        Username = user.Username;
        FirstName = user.LastName;
        LastName = user.LastName;
        LanguageCode = user.LanguageCode;
        IsPremium = user.IsPremium;
        HasBotInAttachmentMenu = user.HasBotInAttachmentMenu;

        FirstInteractionAt = DateTime.UtcNow;

        Chats = new List<Chat>();
        Conversations = new List<Conversation>();
    }

    /// <summary>
    /// Id of the user.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Username of the user.
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// First name of the user.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name of the user.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Language code of the user.
    /// </summary>
    public string? LanguageCode { get; set; }

    /// <summary>
    /// True if the user is premium.
    /// </summary>
    public bool? IsPremium { get; set; }

    /// <summary>
    /// True if the user has added the bot to the attachment menu.
    /// </summary>
    public bool? HasBotInAttachmentMenu { get; set; }

    /// <summary>
    /// Chats related to the user.
    /// </summary>
    public IList<Chat> Chats { get; set; }
    
    /// <summary>
    /// Conversations related to the user.
    /// </summary>
    public ICollection<Conversation> Conversations { get; set; }
    
    /// <summary>
    /// Date of first interaction for this chat.
    /// </summary>
    public DateTime FirstInteractionAt { get; set; }
}