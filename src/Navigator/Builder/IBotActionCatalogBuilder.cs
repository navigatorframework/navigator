using Navigator.Actions;
using Navigator.Context;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Builder;

public interface IBotActionCatalogBuilder
{
    public IBotActionBuilder OnUpdate(Delegate condition, Delegate handler);
    // public IBotActionBuilder OnUpdate(Func<Update, Task<bool>> condition, Delegate handler);
    // public IBotActionBuilder OnUpdate(Func<Update, Task<bool>> condition, Action<INavigatorContext> handler);
    // public IBotActionBuilder OnUpdate(Func<Update, Task<bool>> condition, Func<INavigatorContext, Task> handler);
    // public IBotActionBuilder OnUpdate(Func<Update, bool> condition, Delegate handler);
    // public IBotActionBuilder OnUpdate(Func<Update, bool> condition, Action<INavigatorContext> handler);
    // public IBotActionBuilder OnUpdate(Func<Update, bool> condition, Func<INavigatorContext, Task> handler);


    // public NavigatorBotBuilder OnCommand(string command, Func<INavigatorContext, string[], Task> action);
}