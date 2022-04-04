using System;
using Navigator.Entities;

namespace Navigator.Providers.Telegram.Entities;

public class TelegramChat : Chat
{
    public TelegramChat(long externalIdentifier) : base($"{nameof(TelegramChat)}.{externalIdentifier.ToString()}")
    {
        ExternalIdentifier = externalIdentifier;
    }

    /// <summary>
    /// Telegram identifier for the chat.
    /// </summary>
    public long ExternalIdentifier { get; init; }

    /// <summary>
    /// Title of the chat, if any.
    /// </summary>
    public string? Title { get; init; }
    
    /// <summary>
    /// Type of the chat, can be any of <see cref="TelegramChatType"/>.
    /// </summary>
    public TelegramChatType Type { get; init; }
}

public enum TelegramChatType
{
    Private,
    Group,
    Channel, 
    Supergroup, 
    Sender
}