using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Navigator.Abstraction;
using Telegram.Bot.Types;

namespace Navigator
{
    public class NavigatorContext : INavigatorContext
    {
        protected IDictionary<Type, object> Extensions { get; set; }
        public IBotClient Client { get; }
        public Dictionary<string, string> Items { get; }
        public User BotProfile { get; protected set; }
        public Update Update { get; protected set; }

        public NavigatorContext(IBotClient client)
        {
            Client = client;
            Items = new Dictionary<string, string>();
        }

        public async Task Init(Update update, Dictionary<Type, object> extensions)
        {
            Update = update;
            BotProfile = await Client.GetMeAsync();
            Extensions = new ReadOnlyDictionary<Type, object>(extensions);
        }
        
        public TExtension Get<TExtension>(bool throwIfNotFound)
        {
            if (Extensions.TryGetValue(typeof(TExtension), out var extension))
            {
                if (extension is TExtension castedExtension)
                {
                    return castedExtension;
                }
            }

            return throwIfNotFound 
                ? throw new KeyNotFoundException($"{typeof(TExtension).Name} was not found.") 
                : (TExtension) default;
        }
    }
}