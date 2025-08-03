namespace Navigator.Extensions.Store.Entities;

public class Conversation
{
    protected Conversation() { }
    
    public Conversation(User user)
    {
        User = user;
    }
    
    public Guid Id { get; init; } = Guid.CreateVersion7();
    public User User { get; set; } = null!;
    public Chat? Chat { get; set; }
    
    public DateTimeOffset FirstActiveAt { get; set; } = TimeProvider.System.GetUtcNow();
}