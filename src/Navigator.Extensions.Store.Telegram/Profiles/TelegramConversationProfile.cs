using Navigator.Extensions.Store.Entities;
using Navigator.Providers.Telegram.Entities;

namespace Navigator.Extensions.Store.Telegram.Profiles;

public class TelegramConversationProfile : ConversationProfile
{
    public new TelegramConversation Data { get; set; }
}