using Navigator.Extensions.Store.Entities;

namespace SampleWithCustomStore.Entities;

public class MessageCount
{
    protected MessageCount() {}
    
    public MessageCount(User user)
    {
        User = user;
    }

    public Guid Id { get; init; } = Guid.CreateVersion7();
    public int Count { get; set; }
    
    public User User { get; init; } = null!;
}