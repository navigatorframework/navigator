using Navigator.Actions;
using Navigator.Actions.Builder;
using Telegram.Bot.Types.Enums;

namespace Navigator.Catalog.Factory;

public class BotActionCatalogFactory
{
    private List<BotActionBuilder> Actions { get; } = [];
    private BotActionCatalog? Catalog { get; set; }

    public BotActionBuilder OnUpdate(Delegate condition, Delegate handler)
    {
        var id = Guid.NewGuid();
        var actionBuilder = new BotActionBuilder(condition, handler);

        actionBuilder.SetType(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.Unknown)));

        Actions.Add(actionBuilder);

        return actionBuilder;
    }

    public BotActionCatalog Retrieve()
    {
        if (Catalog is null) Build();

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