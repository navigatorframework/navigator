using Navigator.Actions;
using Telegram.Bot.Types;

namespace Navigator.Strategy.TypeProvider;

public interface IArgumentTypeProvider
{
    public ushort Priority { get; }

    public ValueTask<object?> GetArgument(Type type, Update update, BotAction action);
}