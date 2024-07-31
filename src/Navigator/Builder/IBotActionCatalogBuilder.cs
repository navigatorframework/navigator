using Navigator.Actions;
using Navigator.Context;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Builder;

public interface IBotActionCatalogBuilder
{
    public Dictionary<Guid, BotAction> Actions { get; }
    public Dictionary<Guid, IEnumerable<Type>> ConditionInputTypesByAction { get; }
    public Dictionary<Guid, IEnumerable<Type>> HandlerInputTypesByAction { get; }
    public Dictionary<Guid, string> ActionTypeByAction { get; }
    
    public IBotActionBuilder OnUpdate(Delegate condition, Delegate handler);
    // public IBotActionBuilder OnUpdate(Func<Update, Task<bool>> condition, Delegate handler);
    // public IBotActionBuilder OnUpdate(Func<Update, Task<bool>> condition, Action<INavigatorContext> handler);
    // public IBotActionBuilder OnUpdate(Func<Update, Task<bool>> condition, Func<INavigatorContext, Task> handler);
    // public IBotActionBuilder OnUpdate(Func<Update, bool> condition, Delegate handler);
    // public IBotActionBuilder OnUpdate(Func<Update, bool> condition, Action<INavigatorContext> handler);
    // public IBotActionBuilder OnUpdate(Func<Update, bool> condition, Func<INavigatorContext, Task> handler);


    // public NavigatorBotBuilder OnCommand(string command, Func<INavigatorContext, string[], Task> action);
}