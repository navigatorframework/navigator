using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface IChatHandler<TChat>
    {
        Task<TChat> Find(Chat telegramChat, CancellationToken cancellationToken = default);
        Task Handle(Chat telegramChat, User telegramUser, CancellationToken cancellationToken = default);
    }
}