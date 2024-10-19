using Navigator.Abstractions.Actions;
using Navigator.Abstractions.Catalog;

namespace Navigator.Catalog;

/// <inheritdoc />
public sealed record BotActionCatalog : IBotActionCatalog
{
    private readonly Dictionary<Guid, BotAction> _actions;
    private readonly Dictionary<Guid, ushort> _priorityByAction;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BotActionCatalog" /> class.
    /// </summary>
    /// <param name="actions">The list of <see cref="BotAction" /> instances.</param>
    public BotActionCatalog(IList<BotAction> actions)
    {
        _actions = actions.ToDictionary(action => action.Id);
        _priorityByAction = actions.ToDictionary(action => action.Id, action => action.Information.Priority);
    }

    /// <inheritdoc />
    public IEnumerable<BotAction> Retrieve(UpdateCategory category)
    {
        return _actions.Values
            .Where(action => action.Information.Category == category)
            .OrderBy(action => _priorityByAction[action.Id]);
    }
}