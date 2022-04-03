using Navigator.Entities;
using Navigator.Extensions.Store.Extractors;
using Navigator.Providers.Telegram.Entities;

namespace Navigator.Extensions.Store.Telegram.Extractors;

public class TelegramUserDataExtractor : IDataExtractor
{
    public Dictionary<string, string> From(Conversation source)
    {
        var dict = new Dictionary<string, string>();
        
        if (source.User is TelegramUser user)
        {
            dict.Add($"navigator.telegram.{nameof(TelegramUser.ExternalIdentifier)}", user.ExternalIdentifier.ToString());

            if (user.Username is not null)
            {
                dict.Add($"navigator.telegram.{nameof(TelegramUser.Username)}", user.Username);
            }

            dict.Add($"navigator.telegram.{nameof(TelegramUser.FirstName)}", user.FirstName);

            if (user.LastName is not null)
            {
                dict.Add($"navigator.telegram.{nameof(TelegramUser.LastName)}", user.LastName);
            }
            
            if (user.LanguageCode is not null)
            {
                dict.Add($"navigator.telegram.{nameof(TelegramUser.LanguageCode)}", user.LanguageCode);
            }
        }

        return dict;
    }

    public bool Maps(Type type)
    {
        return type == typeof(TelegramUser);
    }
}