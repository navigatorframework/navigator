using System;

namespace Navigator.Extensions.Store.Entity
{
    public class Conversation
    {
        public Conversation()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public long ChatId { get; set; }
        public Chat Chat { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}