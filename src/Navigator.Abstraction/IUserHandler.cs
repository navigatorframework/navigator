using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Navigator.Abstraction
{
    public interface IUserHandler<TUser>
    {
        Task<TUser> Find(User telegramUser, CancellationToken cancellationToken = default);
        Task Handle(User telegramUser, CancellationToken cancellationToken = default);
        Task Handle(User telegramUser, Chat telegramChat, CancellationToken cancellationToken = default);
    }
}