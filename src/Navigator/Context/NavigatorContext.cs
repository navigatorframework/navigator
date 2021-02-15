using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Navigator.Entities;
using Telegram.Bot.Types;

namespace Navigator.Context
{
    /// <summary>
    /// Navigator Context present in all actions.
    /// </summary>
    public class NavigatorContext
    {
        /// <summary>
        /// 
        /// </summary>
        public readonly INavigatorClient Client;
        public readonly IDictionary<string, object> Extensions;
        public Dictionary<string, string> Items { get; }

        public readonly BotUser BotProfile { get; }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NavigatorContext(INavigatorClient client)
        {
            Client = client;
            BotProfile = client.GetProfile();
            Items = new Dictionary<string, string>();
        }

        /// <inheritdoc />
        public TExtension? Get<TExtension>(string extensionKey, bool throwIfNotFound = false)
        {
            if (Extensions.TryGetValue(extensionKey, out var extension))
            {
                if (extension is TExtension castedExtension)
                {
                    return castedExtension;
                }
            }

            return throwIfNotFound 
                ? throw new KeyNotFoundException($"{typeof(TExtension).Name} was not found.") 
                : default;
        }
    }
}