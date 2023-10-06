using Navigator.Entities;
using Navigator.Extensions.Store.Extractors;
using Navigator.Providers.Telegram.Entities;

namespace Navigator.Extensions.Store.Telegram.Extractors;

public class TelegramConversationDataExtractor : IDataExtractor
{
    public Dictionary<string, string> From(Conversation source)
    {
        var dict = new Dictionary<string, string>();

        return dict;
    }

    public bool Maps(Type type)
    {
        return type == typeof(TelegramConversation);
    }
}