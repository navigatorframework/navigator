using System.Collections.ObjectModel;
using Navigator.Actions;
using Navigator.Actions.Builder;
using Telegram.Bot.Types.Enums;

namespace Navigator.Catalog;

public class BotActionCatalogFactory : IBotActionCatalogFactory
{
    private List<BotActionBuilder> Actions { get; } = [];
    private BotActionCatalog? Catalog { get; set; }

    public IBotActionBuilder OnUpdate(Delegate condition, Delegate handler)
    {
        var id = Guid.NewGuid();
        var actionBuilder = new BotActionBuilder(condition, handler);

        actionBuilder.SetType($"{typeof(UpdateType)}.{nameof(UpdateType.Unknown)}");
        
        Actions.Add(actionBuilder);

        return actionBuilder;
    }

    public BotActionCatalog Retrieve()
    {
        if (Catalog is null)
        {
            Build();
        }

        return Catalog!;
    }

    private void Build()
    {
        var actions = Actions
            .Select(actionBuilder => actionBuilder.Build())
            .ToList();

        Catalog = new BotActionCatalog(actions);
    }
}