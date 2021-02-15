using System.Threading;
using System.Threading.Tasks;
using Navigator.Entities;

namespace Navigator
{
    public interface INavigatorClient
    {
        /// <summary>
        /// Retrieves the bot user information.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<BotUser> GetProfile(CancellationToken cancellationToken = default);

        IProvider Provider();
    }
}