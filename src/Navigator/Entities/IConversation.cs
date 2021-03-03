using System.Collections.Generic;

namespace Navigator.Entities
{
    public interface IConversation
    {
        IUser User { get; init; }
        IChat Chat { get; init; }
    }

    public interface IChat
    {
        string Id { get; init; }
        string? Title { get; init; }
    }
}