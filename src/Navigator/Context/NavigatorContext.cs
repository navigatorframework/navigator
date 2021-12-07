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
        /// Profile of the bot running in this context.
        /// </summary>
        public Bot BotProfile { get; }

        public Dictionary<string, object?> Extensions { get; }

        public Dictionary<string, string> Items { get; }

        public string ActionType { get; }
        
        public Conversation Conversation { get; }

        /// <summary>
        /// Builds a new Navigator Context.
        /// </summary>
        public NavigatorContext(INavigatorProvider provider, Bot botProfile, string actionType, Conversation conversation)
        {
            Provider = provider;
            BotProfile = botProfile;
            ActionType = actionType;
            Conversation = conversation;

            Items = new Dictionary<string, string>();
            Extensions = new Dictionary<string, object?>();
        }
    }
}