using System.Collections.Generic;

namespace Navigator.Extensions.Shipyard.Abstractions.Model
{
    /// <summary>
    /// Bot commands.
    /// </summary>
    public class BotCommands
    {
        /// <summary>
        /// List of commands.
        /// </summary>
        public Dictionary<string, string> Commands { get; init; }
    }
}