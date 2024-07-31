using Navigator.Context;
using Telegram.Bot.Types;

namespace Navigator.Actions;

public record BotAction
{
    private Func<Update, Task<bool>>? ConditionAsync { get; } = default;
    private Func<Update, bool>? Condition { get; } = default;

    private Func<INavigatorContext, Task>? HandlerAsync { get; } = default;
    private Action<INavigatorContext>? Handler { get; } = default;
    
    /// <summary>
    /// Constructor for condition and handler being both async delegates.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="handler"></param>
    public BotAction(Func<Update, Task<bool>> condition, Func<INavigatorContext, Task> handler)
    {
        ConditionAsync = condition;
        HandlerAsync = handler;
    }

    /// <summary>
    /// Constructor for an async condition delegate with a sync handler delegate.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="handler"></param>
    public BotAction(Func<Update, Task<bool>> condition, Action<INavigatorContext> handler)
    {
        ConditionAsync = condition;
        Handler = handler;
    }

    /// <summary>
    /// Constructor for a sync condition delegate with an async handler delegate.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="handler"></param>
    public BotAction(Func<Update, bool> condition, Func<INavigatorContext, Task> handler)
    {
        Condition = condition;
        HandlerAsync = handler;
    }

    /// <summary>
    /// Constructor for condition and handler being both sync delegates.
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="handler"></param>
    public BotAction(Func<Update, bool> condition, Action<INavigatorContext> handler)
    {
        Condition = condition;
        Handler = handler;
    }
}