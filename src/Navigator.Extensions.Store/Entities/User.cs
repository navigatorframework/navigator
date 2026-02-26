namespace Navigator.Extensions.Store.Entities;

/// <summary>
///     Represents a Telegram user persisted in the store.
/// </summary>
public class User
{
    /// <summary>
    ///     EF Core constructor.
    /// </summary>
    protected User() { }
    
    /// <summary>
    ///     Creates a new <see cref="User"/> with the specified Telegram user identifier.
    /// </summary>
    /// <param name="externalId">The Telegram user identifier.</param>
    public User(long externalId)
    {
        ExternalId = externalId;
    }
    
    /// <summary>
    ///     Internal unique identifier.
    /// </summary>
    public Guid Id { get; init; } = Guid.CreateVersion7();

    /// <summary>
    ///     The Telegram user identifier.
    /// </summary>
    public long ExternalId { get; set; }

    /// <summary>
    ///     Conversations this user is part of.
    /// </summary>
    public List<Conversation> Conversations { get; set; } = [];

    /// <summary>
    ///     Timestamp of the first recorded activity for this user.
    /// </summary>
    public DateTimeOffset FirstActiveAt { get; set; } = TimeProvider.System.GetUtcNow();
}
