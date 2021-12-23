using Navigator.Extensions.Store.Entities;
using Navigator.Providers.Telegram.Entities;

namespace Navigator.Extensions.Store.Telegram.Profiles;

public class TelegramUserProfile : UserProfile
{
    public new TelegramUser Data { get; set; }
}