using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Extensions.AI.Models;
using Navigator.Extensions.AI.Services;
using Telegram.Bot.Types;

namespace Navigator.Extensions.AI.Resolvers;

public class ChatContextArgumentResolver : IArgumentResolver
{
    private readonly IChatContextStore _chatContextStore;

    public ChatContextArgumentResolver(IChatContextStore chatContextStore)
    {
        _chatContextStore = chatContextStore;
    }

    public async ValueTask<object?> GetArgument(Type type, Update update, BotAction action)
    {
        return type == typeof(ChatContext)
            ? await _chatContextStore.GetForUpdateAsync(update)
            : null;
    }
}
