using Navigator.Entities;

namespace Navigator.Providers.Telegram.Entities;

public class TelegramConversation : Conversation
{
    public TelegramConversation()
    {
        
    }
    
    public TelegramConversation(TelegramUser user, TelegramChat? chat) : base(user, chat)
    {
        User = user;
        Chat = chat;
    }

    /// <summary>
    /// Telegram user.
    /// </summary>
    public new TelegramUser User { get; init; }

    /// <summary>
    /// Telegram chat.
    /// </summary>
    public new TelegramChat? Chat { get; init; }
}