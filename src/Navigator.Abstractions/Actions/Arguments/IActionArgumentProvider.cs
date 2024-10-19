using Telegram.Bot.Types;

namespace Navigator.Abstractions.Actions.Arguments;

public interface IActionArgumentProvider
{
    public ValueTask<object?[]> GetArguments(Update update, BotAction action);
}