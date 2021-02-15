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

        /// <summary>
        /// 
        /// </summary>
        public readonly BotUser BotProfile;

        /// <summary>
        /// 
        /// </summary>
        public readonly Dictionary<string, string> Items;

        /// <summary>
        /// Builds a new Navigator Context.
        /// </summary>
        public NavigatorContext(INavigatorClient client, BotUser botProfile)
        {
            Client = client;
            BotProfile = botProfile;
            
            Items = new Dictionary<string, string>();
        }
    }
}