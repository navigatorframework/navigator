namespace Navigator.Extensions.Store.Entities;

public class Chat
{
    protected Chat() { }
    
    public Chat(long externalId)
    {
        ExternalId = externalId;
    }
    
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public long ExternalId { get; set; }
    public List<Conversation> Conversations { get; set; } = [];
    
    public DateTimeOffset FirstActiveAt { get; set; } = TimeProvider.System.GetUtcNow();
    public DateTimeOffset LastActiveAt { get; set; } = TimeProvider.System.GetUtcNow();
}