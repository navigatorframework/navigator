using Navigator.Context;
using Telegram.Bot.Types;

namespace Navigator.Configuration;

public interface INavigatorBotBuilder
{
    public NavigatorBotBuilder OnUpdate(Func<Update, Task<bool>> condition, Action<INavigatorContext> action);
    public NavigatorBotBuilder OnUpdate(Func<Update, bool> condition, Action<INavigatorContext> action);
}