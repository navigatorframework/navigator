using Navigator.Entities;

namespace Navigator.Context;

public interface INavigatorContext
{
    /// <summary>
    /// 
    /// </summary>
    Bot BotProfile { get; }

    /// <summary>
    /// 
    /// </summary>
    Dictionary<string, object?> Extensions { get; }

    /// <summary>
    /// 
    /// </summary>
    Dictionary<string, string> Items { get; }
        
    /// <summary>
    /// 
    /// </summary>
    public string ActionType { get; }
        
    public Conversation Conversation { get; }
}