using System;
using System.Collections.Generic;

namespace Navigator.Extensions.Store.Abstractions.Entity
{
    public class User
    {
        public User()
        {
            Conversations = new List<Conversation>();
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Id of the user.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Specifies if the user is a bot or not.
        /// </summary>
        public bool IsBot { get; set; }
        /// <summary>
        /// Language code of the user client.
        /// </summary>
        public string? LanguageCode { get; set; }
        /// <summary>
        /// Username of the user.
        /// </summary>
        public string? Username { get; set; }
        /// <summary>
        /// List of conversations that this user has.
        /// </summary>
        public List<Conversation> Conversations { get; set; }
        /// <summary>
        /// Date of first interaction with the user.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}