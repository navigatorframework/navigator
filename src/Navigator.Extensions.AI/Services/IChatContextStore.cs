using Navigator.Extensions.AI.Models;
using Telegram.Bot.Types;

namespace Navigator.Extensions.AI.Services;

public interface IChatContextStore
{
    Task AppendIncomingMessageAsync(Message message, CancellationToken cancellationToken = default);

    Task<ChatContext> GetForUpdateAsync(Update update, CancellationToken cancellationToken = default);

    ChatContext CreateEmptyContext();
}
