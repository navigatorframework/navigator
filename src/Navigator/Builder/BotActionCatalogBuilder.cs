using Navigator.Actions;
using Navigator.Context;
using Telegram.Bot.Types;

namespace Navigator.Builder;

public class BotActionCatalogBuilder : IBotActionCatalogBuilder
{
    protected Dictionary<Guid, BotAction> Actions = [];
    protected Dictionary<Guid, IEnumerable<Type>> ConditionInputTypesByAction = [];
    protected Dictionary<Guid, IEnumerable<Type>> HandlerInputTypesByAction = [];
    
    public IBotActionBuilder OnUpdate(Delegate condition, Delegate handler)
    {
        var id = Guid.NewGuid();
        var action = new BotAction(condition, handler);
        
        Actions.Add(id, action);
        
        ConditionInputTypesByAction.Add(id, condition.Method.GetParameters().Select(a => a.ParameterType));
        HandlerInputTypesByAction.Add(id, handler.Method.GetParameters().Select(a => a.ParameterType));

        return new BotActionBuilder(action);
    }
}