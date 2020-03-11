using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface INavigatorContext
    {
        Dictionary<string, string> Items { get; }
        public IBotClient Client { get; }
        public User BotProfile { get; }
        public Update Update { get; }

        Task Init(Update update);
        TExtension Get<TExtension>(bool throwIfNotFound);
    }
}