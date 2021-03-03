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

        public Dictionary<string, string> Items { get; }

        public string ActionType { get; }
        
        public IConversation Conversation { get; }

        /// <summary>
        /// Builds a new Navigator Context.
        /// </summary>
        public NavigatorContext(INavigatorProvider provider, BotUser botProfile, string actionType, IConversation conversation)
        {
            Provider = provider;
            BotProfile = botProfile;
            ActionType = actionType;
            Conversation = conversation;

            Items = new Dictionary<string, string>();
        }
    }
}