using Navigator.Context;
using Telegram.Bot.Types;

namespace Navigator.Actions;

public record BotAction
{
    // private Func<Update, Task<bool>>? ConditionAsync { get; } = default;
    // private Func<Update, bool>? Condition { get; } = default;
    //
    // private Func<INavigatorContext, Task>? HandlerAsync { get; } = default;
    // private Action<INavigatorContext>? Handler { get; } = default;
    
    private static Delegate Condition { get; set; }
    private static Delegate Handler { get; set; }

    public BotAction(Delegate condition, Delegate handler)
    {
        Condition = condition;
        Handler = handler;
    }
    
    public async Task ExecuteCondition(params object[] args)
    {
        var result = Handler.DynamicInvoke(args);

        if (result is Task task)
        {
            await task;
        }
    }

    public async Task ExecuteHandler(params object[] args)
    {
        var result = Handler.DynamicInvoke(args);

        if (result is Task task)
        {
            await task;
        }
    }
}