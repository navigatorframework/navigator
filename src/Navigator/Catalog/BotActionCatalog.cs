using Navigator.Actions;

namespace Navigator.Catalog;

public record BotActionCatalog
{
    protected readonly Dictionary<Guid, BotAction> Actions;
    protected readonly Dictionary<Guid, ushort> PriorityByAction;
    protected readonly Dictionary<string, Guid[]> ActionsByType;
    
    public BotActionCatalog(IList<BotAction> actions)
    {
        Actions = actions.ToDictionary(action => action.Id);
        PriorityByAction = actions.ToDictionary(action => action.Id, action => action.Information.Priority);
        ActionsByType = actions.GroupBy(action => action.Information.ActionType)
            .ToDictionary(grouping => grouping.Key, grouping => grouping.Select(action => action.Id).ToArray());
    }
    
    //TODO: rework this into IAsyncEnumerable and yield
    public IEnumerable<BotAction> Retrieve(string type)
    {
        if (ActionsByType.TryGetValue(type, out var filtered) is false)
        {
            return [];
        }

        var actions = filtered.Select(id => Actions[id]).OrderBy(action => PriorityByAction[action.Id]);

        return actions;
    }
}