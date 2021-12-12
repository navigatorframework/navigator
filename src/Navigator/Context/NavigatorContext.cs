using System.Collections.Generic;
using Navigator.Entities;

namespace Navigator.Context;

internal class NavigatorContext : INavigatorContext
{
    public INavigatorProvider Provider { get; }
    public Bot BotProfile { get; }
    public Dictionary<string, object?> Extensions { get; }
    public Dictionary<string, string> Items { get; }
    public string ActionType { get; }
    public Conversation Conversation { get; }

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