using Telegram.Bot.Types;

namespace Navigator.Strategy;

public interface INavigatorStrategy
{
    public Task Invoke(Update update);
}