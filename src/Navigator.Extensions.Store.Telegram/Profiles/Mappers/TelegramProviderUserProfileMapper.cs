using Navigator.Entities;
using Navigator.Extensions.Store.Entities;
using Navigator.Extensions.Store.Mappers;
using Navigator.Providers.Telegram.Entities;

namespace Navigator.Extensions.Store.Telegram.Profiles.Mappers;

public class TelegramProviderUserProfileMapper : IProviderUserProfileMapper
{
    public UserProfile? From(Conversation sourceConversation)
    {
        if (sourceConversation.User is TelegramUser user)
        {
            return new TelegramUserProfile
            {
                Id = Guid.NewGuid(),
                Identification = sourceConversation.User.Id,
                Data = user,
                DataId = sourceConversation.User.Id
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
        return type == typeof(TelegramUser) || type == typeof(TelegramBot);
    }
}