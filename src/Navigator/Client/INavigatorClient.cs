using Navigator.Entities;
using Telegram.Bot;

namespace Navigator.Client;

public interface INavigatorClient : ITelegramBotClient
{
    /// <summary>
    /// Retrieves the bot user information.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Bot> GetProfile(CancellationToken cancellationToken = default);
}