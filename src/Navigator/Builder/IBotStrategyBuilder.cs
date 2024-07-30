using Navigator.Context;
using Telegram.Bot.Types;

namespace Navigator.Builder;

public interface IBotStrategyBuilder
{
    public IBotActionBuilder OnUpdate(Func<Update, Task<bool>> condition, Func<INavigatorContext, Task> handler);
    public IBotActionBuilder OnUpdate(Func<Update, Task<bool>> condition, Action<INavigatorContext> handler);
    public IBotActionBuilder OnUpdate(Func<Update, bool> condition, Func<INavigatorContext, Task> handler);
    public IBotActionBuilder OnUpdate(Func<Update, bool> condition, Action<INavigatorContext> handler);
    
    // public NavigatorBotBuilder OnCommand(string command, Func<INavigatorContext, string[], Task> action);
}