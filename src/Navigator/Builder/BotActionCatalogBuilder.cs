using Navigator.Actions;
using Navigator.Context;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Builder;

public class BotActionCatalogBuilder : IBotActionCatalogBuilder
{
    public Dictionary<Guid, BotAction> Actions { get; } = [];
    public Dictionary<Guid, IEnumerable<Type>> ConditionInputTypesByAction { get; }  = [];
    public Dictionary<Guid, IEnumerable<Type>> HandlerInputTypesByAction { get; }  = [];
    public Dictionary<Guid, string> ActionTypeByAction { get; }  = [];
    
    public IBotActionBuilder OnUpdate(Delegate condition, Delegate handler)
    {
        var id = Guid.NewGuid();
        var action = new BotAction(condition, handler);
        
        Actions.Add(id, action);
        
        ConditionInputTypesByAction.Add(id, condition.Method.GetParameters().Select(a => a.ParameterType));
        HandlerInputTypesByAction.Add(id, handler.Method.GetParameters().Select(a => a.ParameterType));

        ActionTypeByAction.Add(id, $"{typeof(UpdateType)}.{nameof(UpdateType.Unknown)}");
        
        return new BotActionBuilder(action);
    }
}