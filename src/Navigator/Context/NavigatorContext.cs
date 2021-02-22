using System.Collections.Generic;
using Navigator.Entities;

namespace Navigator.Context
{
    /// <summary>
    /// Navigator Context present in all actions.
    /// </summary>
    public class NavigatorContext : INavigatorContext
    {
        /// <summary>
        /// 
        /// </summary>
        public INavigatorClient Client { get; }

        /// <summary>
        /// 
        /// </summary>
        public BotUser BotProfile { get; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> Items { get; }

        /// <summary>
        /// 
        /// </summary>
        public string ActionType { get; }
        
        /// <summary>
        /// Builds a new Navigator Context.
        /// </summary>
        public NavigatorContext(INavigatorClient client, BotUser botProfile, string actionType)
        {
            Client = client;
            BotProfile = botProfile;
            ActionType = actionType;

            Items = new Dictionary<string, string>();
        }
    }
}