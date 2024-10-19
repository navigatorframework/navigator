using Navigator.Abstractions.Actions;
using Telegram.Bot.Types;

namespace Navigator.Strategy.Context;

/// <summary>
///     Context around an <see cref="Update" /> and a specific <see cref="BotAction" /> for a <see cref="NavigatorActionExecutionContext" />.
/// </summary>
public record NavigatorActionExecutionContext
{
    /// <summary>
    ///     The <see cref="BotAction" /> object that is relevant to this execution.
    /// </summary>
    public readonly BotAction Action;

    /// <summary>
    ///     The <see cref="UpdateCategory" /> of the <see cref="Update" />.
    /// </summary>
    public readonly UpdateCategory Category;

    /// <summary>
    ///     Collection of items that can be accessed during each step of the strategy.
    /// </summary>
    public readonly Dictionary<object, object?> Items;

    /// <summary>
    ///     The <see cref="Update" /> object that triggered the execution of the <see cref="BotAction" />.
    /// </summary>
    public readonly Update Update;


    public NavigatorActionExecutionContext(BotAction action, UpdateCategory category, Dictionary<object, object?> items, Update update)
    {
        Action = action;
        Category = category;
        Items = items;
        Update = update;
    }
}