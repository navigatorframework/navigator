using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Navigator.Entities;
using Telegram.Bot.Types;

namespace Navigator.Old
{
    /// <summary>
    /// Navigator Context present in all actions.
    /// </summary>
    public class NavigatorContext : INavigatorContext
    {
        /// <summary>
        /// Extensions.
        /// </summary>
        protected IDictionary<string, object> Extensions { get; set; } = null!;

        /// <inheritdoc />
        public INavigatorClient Client { get; }

        /// <inheritdoc />
        public Dictionary<string, string> Items { get; }

        /// <inheritdoc />
        public BotUser BotProfile { get; protected set; } = null!;

        /// <inheritdoc />
        public Update Update { get; protected set; } = null!;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="client"></param>
        public NavigatorContext(INavigatorClient client)
        {
            Client = client;
            Items = new Dictionary<string, string>();
        }

        /// <inheritdoc />
        public async Task Init(Update update, Dictionary<string, object> extensions)
        {
            Update = update;
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