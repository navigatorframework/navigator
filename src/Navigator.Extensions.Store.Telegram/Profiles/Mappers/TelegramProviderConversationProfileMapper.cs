using Navigator.Entities;
using Navigator.Extensions.Store.Entities;
using Navigator.Extensions.Store.Mappers;
using Navigator.Providers.Telegram.Entities;
using Conversation = Navigator.Entities.Conversation;

namespace Navigator.Extensions.Store.Telegram.Profiles.Mappers;

public class TelegramProviderConversationProfileMapper : IProviderConversationProfileMapper
{
    public ConversationProfile? From(Conversation sourceConversation)
    {
        if (sourceConversation is TelegramConversation conversation)
        {
            return new TelegramConversationProfile
            {
                Id = Guid.NewGuid(),
                Identification = sourceConversation.Id,
                Data = conversation,
                DataId = sourceConversation.Id
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
        return type == typeof(TelegramConversation);
    }
}