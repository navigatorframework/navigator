﻿using Navigator.Client;
using Navigator.Entities;

namespace Navigator.Context;

internal class NavigatorContext : INavigatorContext
{
    public INavigatorClient Client { get; }
    public Bot BotProfile { get; }
    public Dictionary<string, object?> Extensions { get; }
    public Dictionary<string, string> Items { get; }
    public string ActionType { get; }
    public Conversation Conversation { get; }

    public NavigatorContext(INavigatorClient navigatorClient, Bot botProfile, string actionType, Conversation conversation)
    {
        Client = navigatorClient;
        BotProfile = botProfile;
        ActionType = actionType;
        Conversation = conversation;

        Items = new Dictionary<string, string>();
        Extensions = new Dictionary<string, object?>();
    }
}