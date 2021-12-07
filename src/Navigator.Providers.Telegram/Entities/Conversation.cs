using Navigator.Entities;

namespace Navigator.Providers.Telegram.Entities
{
    public record TelegramConversation : Conversation
    {
        /// <summary>
        /// Telegram user.
        /// </summary>
        public new TelegramUser User { get; init; }

        /// <summary>
        /// Telegram chat.
        /// </summary>
        public new TelegramChat Chat { get; init; }
    }
}