using Navigator.Context;
using Telegram.Bot.Types;

namespace Navigator.Configuration;

public class NavigatorBotBuilder : INavigatorBotBuilder
{
    protected readonly List<BotAction> Actions = [];
    public NavigatorBotBuilder OnUpdate(Func<Update, Task<bool>> condition, Action<INavigatorContext> action)
    {
        Actions.Add(new BotAction(condition, action));

        return this;
    }

    public NavigatorBotBuilder OnUpdate(Func<Update, bool> condition, Action<INavigatorContext> action)
    {
        throw new NotImplementedException();
    }
}