using Navigator.Extensions.Store.Entities;
using Navigator.Providers.Telegram.Entities;

namespace Navigator.Extensions.Store.Telegram;

public class TelegramChatProfile : ChatProfile
{
    public new TelegramChat Data { get; set; }
}