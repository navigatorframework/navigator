using System;
using Navigator.Entities;

namespace Navigator.Providers.Telegram.Entities;

public record TelegramChat(long ExternalIdentifier) : Chat(ExternalIdentifier.ToString())
{
    /// <summary>
    /// Telegram identifier for the user.
    /// </summary>
    public long ExternalIdentifier { get; init; } = ExternalIdentifier;

    /// <summary>
    /// Title of the chat, if any.
    /// </summary>
    public string? Title { get; init; }
}