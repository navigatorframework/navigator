using Navigator.Abstractions.Entities;
using Telegram.Bot;

namespace Navigator.Abstractions.Client;

/// <summary>
/// Navigator Client.
/// </summary>
public interface INavigatorClient : ITelegramBotClient
{
    /// <summary>
    /// Retrieves the bot user information.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Bot> GetProfile(CancellationToken cancellationToken = default);
}