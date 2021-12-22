namespace Navigator.Extensions.Store.Entities;

public class UniversalProfile
{
    protected UniversalProfile()
    {
        CreatedAt = DateTime.UtcNow;
        LastUpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public string Provider { get; set; }
    public Guid Identification { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}

public class UserProfile : UniversalProfile
{
    
}

public class ChatProfile : UniversalProfile
{
    
}

public class ConversationProfile : UniversalProfile
{
    
}