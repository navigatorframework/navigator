namespace Navigator.Extensions.Store.Entities;

public class User
{
    protected User() { }
    
    public User(long externalId)
    {
        ExternalId = externalId;
    }
    
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public long ExternalId { get; set; }
    public List<Conversation> Conversations { get; set; } = [];

    public DateTimeOffset FirstActiveAt { get; set; } = TimeProvider.System.GetUtcNow();
    public DateTimeOffset LastActiveAt { get; set; } = TimeProvider.System.GetUtcNow();
}