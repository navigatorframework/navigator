using System.Threading;
using System.Threading.Tasks;
using Navigator.Extensions.Store.Abstractions.Entity;

namespace Navigator.Extensions.Store.Abstractions
{
    public interface IEntityManager<TUser, TChat>
        where TUser : User
        where TChat : Chat
    {
        Task Handle(Telegram.Bot.Types.User telegramUser, CancellationToken cancellationToken = default);
        Task Handle(Telegram.Bot.Types.User telegramUser, Telegram.Bot.Types.Chat telegramChat, CancellationToken cancellationToken = default);
        TUser FindUser(int id);
        Task<TUser> FindUserAsync(int id, CancellationToken cancellationToken = default);
        TChat FindChat(long id);
        Task<TChat> FindChatAsync(long id, CancellationToken cancellationToken = default);
        Task MigrateFromGroup(Telegram.Bot.Types.Message telegramMessage, CancellationToken cancellationToken = default);
        Task MigrateToSupergroup(Telegram.Bot.Types.Message telegramMessage, CancellationToken cancellationToken = default);
        Task ChatTitleChanged(Telegram.Bot.Types.Message telegramMessage, CancellationToken cancellationToken = default);
        Task ChatMemberLeft(Telegram.Bot.Types.Message telegramMessage, CancellationToken cancellationToken = default);
    }
}