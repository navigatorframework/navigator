using Navigator.Entities;

namespace Navigator.Providers.Telegram.Entities
{
    public record TelegramConversation(TelegramUser User, TelegramChat Chat) : Conversation(User, Chat)
    {
        /// <summary>
        /// Telegram user.
        /// </summary>
        public new TelegramUser User { get; init; } = User;

        /// <summary>
        /// Telegram chat.
        /// </summary>
        public new TelegramChat Chat { get; init; } = Chat;
    }
}