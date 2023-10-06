using Navigator.Entities;
using Navigator.Extensions.Store.Extractors;
using Navigator.Providers.Telegram.Entities;

namespace Navigator.Extensions.Store.Telegram.Extractors;

public class TelegramChatDataExtractor : IDataExtractor
{
    public Dictionary<string, string> From(Conversation source)
    {
        var dict = new Dictionary<string, string>();
        
        if (source.Chat is TelegramChat chat)
        {
            dict.Add($"navigator.telegram.{nameof(TelegramChat.ExternalIdentifier)}", chat.ExternalIdentifier.ToString());

            if (chat.Title is not null)
            {
                dict.Add($"navigator.telegram.{nameof(TelegramChat.Title)}", chat.Title);
            }
        }

        return dict;
    }

    public bool Maps(Type type)
    {
        return type == typeof(TelegramChat);
    }
}