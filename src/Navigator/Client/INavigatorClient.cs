using Navigator.Entities;

namespace Navigator.Client;

public interface INavigatorClient
{
    /// <summary>
    /// Retrieves the bot user information.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Bot> GetProfile(CancellationToken cancellationToken = default);
}