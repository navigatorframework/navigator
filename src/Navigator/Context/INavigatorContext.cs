using System.Collections.Generic;
using Navigator.Entities;

namespace Navigator.Context
{
    public interface INavigatorContext
    {
        /// <summary>
        /// 
        /// </summary>
        INavigatorClient Client { get; }

        /// <summary>
        /// 
        /// </summary>
        BotUser BotProfile { get; }

        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, string> Items { get; }
    }
}