using Navigator.Actions;

namespace Navigator.Catalog;

public record BotActionCatalog
{
    protected readonly Dictionary<Guid, BotAction> Actions;
    protected readonly Dictionary<Guid, ushort> PriorityByAction;
    
    public BotActionCatalog(IList<BotAction> actions)
    {
        Actions = actions.ToDictionary(action => action.Id);
        PriorityByAction = actions.ToDictionary(action => action.Id, action => action.Information.Priority);
    }
    
    //TODO: rework this into IAsyncEnumerable and yield
    public IEnumerable<BotAction> Retrieve(UpdateCategory category)
    {
        return Actions.Values
            .Where(action => action.Information.Category == category)
            .OrderBy(action => PriorityByAction[action.Id]);
    }
}