using Navigator.Abstractions.Actions;
using Navigator.Actions;
using Navigator.Telegram;
using Telegram.Bot.Types;

namespace Navigator.Strategy.TypeProvider;

internal sealed record TelegramEntitiesTypeProvider : IArgumentTypeProvider
{
    /// <inheritdoc />
    public ushort Priority { get; } = 10000;

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