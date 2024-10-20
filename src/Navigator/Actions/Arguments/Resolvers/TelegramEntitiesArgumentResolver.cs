using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Actions.Arguments;
using Navigator.Telegram;
using Telegram.Bot.Types;

namespace Navigator.Actions.Arguments.Resolvers;

internal sealed record TelegramEntitiesArgumentResolver : IArgumentResolver
{
    /// <inheritdoc />
    public ushort Priority => 10000;

    public ValueTask<object?> GetArgument(Type type, Update update, BotAction action)
    {
        return ValueTask.FromResult<object?>(type switch
        {
            not null when type == typeof(Update)
                => update,
            not null when type == typeof(Chat)
                => update.GetChatOrDefault(),
            not null when type == typeof(User)
                => update.GetUserOrDefault(),
            _ => default
        });
    }
}