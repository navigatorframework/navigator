using Telegram.Bot.Types;

namespace Navigator.Abstractions.Actions.Arguments;

public interface IActionArgumentProvider
{
    public ValueTask<object?> GetArgument(Type type, Update update, BotAction action);
}