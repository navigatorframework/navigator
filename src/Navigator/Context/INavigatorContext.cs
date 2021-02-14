using System.Collections.Generic;
using System.Threading.Tasks;
using Navigator.Entities;
using Telegram.Bot.Types;

namespace Navigator.Context
{
    public interface INavigatorContext
    {
        Dictionary<string, string> Items { get; }
        public INavigatorClient Client { get; }
        public BotUser BotProfile { get; }
        public Update Update { get; }

        Task Init(Update update, Dictionary<string, object> extensions);
        TExtension? Get<TExtension>(string extensionKey, bool throwIfNotFound = false);
    }
}