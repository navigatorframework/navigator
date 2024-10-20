using Telegram.Bot.Types;

namespace Navigator.Abstractions.Actions.Arguments;

public interface IActionArgumentProvider
{
    public ValueTask<object?[]> GetConditionArguments(Update update, BotAction action);
    public ValueTask<object?[]> GetHandlerArguments(Update update, BotAction action);
}