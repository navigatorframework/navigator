using Telegram.Bot.Types;

namespace Navigator.Abstractions.Actions.Arguments;

public interface IArgumentResolver
{
    public ValueTask<object?> GetArgument(Type type, Update update, BotAction action);
}