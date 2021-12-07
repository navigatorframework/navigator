using Navigator.Entities;

namespace Navigator.Providers.Telegram.Entities;

public record TelegramBot(long ExternalIdentifier) : Bot(ExternalIdentifier.ToString())
{
    /// <summary>
    /// Telegram identifier for the bot.
    /// </summary>
    public long ExternalIdentifier { get; init; } = ExternalIdentifier;
    
    /// <summary>
    /// Username of the bot.
    /// </summary>
    public string Username { get; init; }
}