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
        public INavigatorProvider Provider { get; }

        /// <summary>
        /// 
        /// </summary>
        public BotUser BotProfile { get; }

        public Dictionary<string, object?> Extensions { get; }

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
        public NavigatorContext(INavigatorProvider provider, BotUser botProfile, string actionType)
        {
            Provider = provider;
            BotProfile = botProfile;
            ActionType = actionType;

            Items = new Dictionary<string, string>();
        }
    }
}