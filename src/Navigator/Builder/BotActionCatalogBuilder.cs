using Navigator.Actions;
using Navigator.Configuration;
using Navigator.Context;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Navigator.Builder;

public class BotActionCatalogBuilder : IBotActionCatalogBuilder
{
    public Dictionary<Guid, BotActionBuilder> Actions { get; } = [];
    
    public IBotActionBuilder OnUpdate(Delegate condition, Delegate handler)
    {
        var id = Guid.NewGuid();
        var actionBuilder = new BotActionBuilder(new BotAction(condition, handler));
        
        Actions.Add(id, actionBuilder);
        
        return actionBuilder;
    }
}