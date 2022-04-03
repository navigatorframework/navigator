using Navigator.Entities;
using Navigator.Extensions.Store.Entities;
using Navigator.Extensions.Store.Mappers;
using Navigator.Providers.Telegram.Entities;

namespace Navigator.Extensions.Store.Telegram.Profiles.Mappers;

public class TelegramProviderChatProfileMapper : IProviderChatProfileMapper
{
    public ChatProfile? From(Conversation sourceConversation)
    {
        if (sourceConversation.Chat is TelegramChat chat)
        {
            return new TelegramChatProfile
            {
                Id = Guid.NewGuid(),
                Identification = sourceConversation.Chat.Id,
                Data = chat,
                DataId = sourceConversation.Chat.Id
            };
        }

        return default;
    }

    UniversalProfile? IProviderProfileMapper.From(Conversation sourceConversation)
    {
        return From(sourceConversation);
    }

    public bool Maps(Type type)
    {
        return type == typeof(TelegramChat);
    }
}