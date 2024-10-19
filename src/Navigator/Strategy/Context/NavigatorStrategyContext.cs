using Navigator.Abstractions.Actions;
using Navigator.Strategy.Pipelines.Bundled;
using Telegram.Bot.Types;

namespace Navigator.Strategy.Context;

/// <summary>
///     Context around an <see cref="Update" /> for a <see cref="DefaultNavigatorPipelineBuilder" />.
/// </summary>
public record NavigatorStrategyContext
{
    /// <summary>
    ///     The list of <see cref="BotAction" /> objects that are relevant to the <see cref="Update" />.
    /// </summary>
    public readonly List<BotAction> Actions = [];

    /// <summary>
    ///     Collection of items that can be accessed during each step of the strategy.
    /// </summary>
    public readonly Dictionary<object, object?> Items = [];

    /// <summary>
    ///     The <see cref="Update" /> object that triggered the execution of the <see cref="DefaultNavigatorPipelineBuilder" />.
    /// </summary>
    public readonly Update Update;

    /// <summary>
    ///     Default constructor.
    /// </summary>
    /// <param name="update">An <see cref="Update" /> object.</param>
    public NavigatorStrategyContext(Update update)
    {
        Update = update;
    }

    /// <summary>
    ///     The <see cref="UpdateCategory" /> of the <see cref="Update" />.
    /// </summary>
    public UpdateCategory UpdateCategory { get; set; } = UpdateCategory.Unknown;
}

/// <summary>
///     Extension methods for <see cref="NavigatorStrategyContext" />.
/// </summary>
public static class NavigatorStrategyContextExtensions
{
    /// <summary>
    ///     Get all <see cref="NavigatorActionExecutionContext" /> for the given <see cref="NavigatorStrategyContext" />.
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static IEnumerable<NavigatorActionExecutionContext> GetExecutionContexts(this NavigatorStrategyContext context)
    {
        return context.Actions.Select(action => new NavigatorActionExecutionContext(action, context.UpdateCategory, context.Items, context.Update));
    }
}