﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstractions
{
    public interface INavigatorContext
    {
        Dictionary<string, string> Items { get; }
        public IBotClient Client { get; }
        public User BotProfile { get; }
        public Update Update { get; }

        Task Init(Update update, Dictionary<string, object> extensions);
        TExtension? Get<TExtension>(string extensionKey, bool throwIfNotFound = false);
    }
}