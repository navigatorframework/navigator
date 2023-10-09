using Navigator.Client;
using Navigator.Entities;

namespace Navigator.Context;

public interface INavigatorContext
{
    public INavigatorClient Client { get; }
    Bot BotProfile { get; }
    Dictionary<string, object?> Extensions { get; }
    Dictionary<string, string> Items { get; }
    public string ActionType { get; }
    public Conversation Conversation { get; }
}