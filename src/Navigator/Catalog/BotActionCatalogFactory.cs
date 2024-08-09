using System.Collections.ObjectModel;
using Navigator.Actions;
using Navigator.Actions.Builder;

namespace Navigator.Catalog;

public class BotActionCatalogFactory : IBotActionCatalogFactory
{
    public List<BotActionBuilder> Actions { get; } = [];

    public IBotActionBuilder OnUpdate(Delegate condition, Delegate handler)
    {
        var id = Guid.NewGuid();
        var actionBuilder = new BotActionBuilder(condition, handler);

        Actions.Add(actionBuilder);

        return actionBuilder;
    }

    public BotActionCatalog Build()
    {
        var actions = Actions
            .Select(actionBuilder => actionBuilder.Build())
            .ToList();
        
        return new BotActionCatalog(actions);
    }
}