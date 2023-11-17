using Navigator.Client;
using Navigator.Entities;

namespace Navigator.Context;

public interface INavigatorContext
{
    /// <summary>
    /// Client to use for interacting with Navigator and the provider (Telegram).
    /// </summary>
    public INavigatorClient Client { get; }
    
    /// <summary>
    /// Profile of the bot in <see cref="Bot"/> form.
    /// </summary>
    Bot BotProfile { get; }
    
    /// <summary>
    /// Installed extensions for <see cref="INavigatorContext"/>
    /// </summary>
    Dictionary<string, object?> Extensions { get; }
    
    /// <summary>
    /// Useful items stored here.
    /// </summary>
    Dictionary<string, string> Items { get; }
    
    /// <summary>
    /// Type of the action for this context.
    /// </summary>
    public string ActionType { get; }
    
    /// <summary>
    /// Conversation related to this context.
    /// </summary>
    public Conversation Conversation { get; }
}