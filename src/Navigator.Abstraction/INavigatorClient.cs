using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Navigator.Abstraction
{
    public interface INavigatorClient : ITelegramBotClient
    {
        Task Start(CancellationToken cancellationToken = default);
    }
}