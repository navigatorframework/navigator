using Navigator.Entities;

namespace Navigator.Providers.Telegram.Entities
{
    public class Conversation : IConversation
    {
        public IUser User { get; init; }
        public IChat Chat { get; init; }
    }

    public class User : IUser
    {
        public string Id { get; init; }
        public string? Username { get; init; }
    }
    
    public class Chat : IChat
    {
        public string Id { get; init; }
        public string? Title { get; init; }
    }
}