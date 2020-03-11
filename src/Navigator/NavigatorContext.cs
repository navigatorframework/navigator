using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Navigator.Abstraction;
using Telegram.Bot.Types;

namespace Navigator
{
    public class NavigatorContext
    {
        protected readonly Dictionary<Type, object> Extensions;
        public readonly IBotClient Client;
        public readonly User BotProfile;
        public readonly Update Update;

        public NavigatorContext(Dictionary<Type, object> extensions, IBotClient client, User botProfile, Update update)
        {
            Extensions = extensions;
            Client = client;
            BotProfile = botProfile;
            Update = update;
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