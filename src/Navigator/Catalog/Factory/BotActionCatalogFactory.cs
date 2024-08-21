using Microsoft.Extensions.Logging;
using Navigator.Actions;
using Navigator.Actions.Builder;
using Telegram.Bot.Types.Enums;

namespace Navigator.Catalog.Factory;

/// <summary>
///     Factory for <see cref="BotActionCatalog" />.
/// </summary>
public class BotActionCatalogFactory
{
    private readonly ILogger<BotActionCatalogFactory> _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="BotActionCatalogFactory" /> class.
    /// </summary>
    /// <param name="logger">An instance of <see cref="ILogger{TCategoryName}" />.</param>
    public BotActionCatalogFactory(ILogger<BotActionCatalogFactory> logger)
    {
        _logger = logger;
    }

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
        _logger.LogInformation("Building BotActionCatalog with {ActionsCount} actions", Actions.Count);

        var actions = new List<BotAction>();

        foreach (var builtAction in Actions.Select(actionBuilder => actionBuilder.Build()))
        {
            _logger.LogDebug("Built action {ActionName} with priority {Priority} for category {Category}",
                builtAction.Name, builtAction.Information.Priority, builtAction.Information.Category);
            
            actions.Add(builtAction);
        }

        Catalog = new BotActionCatalog(actions);
    }
}