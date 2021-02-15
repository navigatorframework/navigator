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
        public readonly IDictionary<string, object> Extensions;
        public readonly INavigatorClient Client;
        public Dictionary<string, string> Items { get; }

        /// <inheritdoc />
        public BotUser BotProfile { get; private set; } = null!;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NavigatorContext(IHttpContextAccessor)
        {
            Items = new Dictionary<string, string>();
        }

        /// <inheritdoc />
        public async Task Init(INavigatorClient client, Dictionary<string, object> extensions)
        {
            Client = client;
            BotProfile = await Client.GetBotUser();
            Extensions = new ReadOnlyDictionary<string, object>(extensions);
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