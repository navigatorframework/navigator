using Navigator.Actions;
using Navigator.Actions.Builder;
using Telegram.Bot.Types.Enums;

namespace Navigator.Catalog.Factory;

/// <summary>
///     Factory for <see cref="BotActionCatalog" />.
/// </summary>
public class BotActionCatalogFactory
{
    private List<BotActionBuilder> Actions { get; } = [];
    private BotActionCatalog? Catalog { get; set; }

    /// <summary>
    ///     Adds a new <see cref="BotActionBuilder" /> to the catalog.
    /// </summary>
    /// <param name="condition">
    ///     A delegate representing the condition under which the handler should be invoked.
    ///     Must return <see cref="bool" /> or <see cref="Task{TResult}" /> where TResult is <see cref="bool" />.
    /// </param>
    /// <param name="handler">
    ///     A delegate representing the action to take when the condition is met.
    /// </param>
    public BotActionBuilder OnUpdate(Delegate condition, Delegate? handler = default)
    {
        var id = Guid.NewGuid();
        var actionBuilder = new BotActionBuilder();

        actionBuilder
            .SetCondition(condition)
            .SetHandler(handler ?? (Action)(() => { }))
            .SetCategory(new UpdateCategory(nameof(UpdateType), nameof(UpdateType.Unknown)));

        Actions.Add(actionBuilder);

        return actionBuilder;
    }

    /// <summary>
    ///     Retrieves the built <see cref="BotActionCatalog" />.
    /// </summary>
    /// <returns>The built <see cref="BotActionCatalog" />.</returns>
    public BotActionCatalog Retrieve()
    {
        if (Catalog is null) Build();

        return Catalog!;
    }

    /// <summary>
    ///     Builds the <see cref="BotActionCatalog" />.
    /// </summary>
    private void Build()
    {
        var actions = Actions
            .Select(actionBuilder => actionBuilder.Build())
            .ToList();

        Catalog = new BotActionCatalog(actions);
    }
}