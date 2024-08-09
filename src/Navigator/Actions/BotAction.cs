using Navigator.Context;
using Telegram.Bot.Types;

namespace Navigator.Actions;

public record BotAction
{
    public readonly Guid Id; 
    public readonly BotActionInformation Information;
    private static Delegate Condition;
    private static Delegate Handler;

    public BotAction(Guid id, BotActionInformation information, Delegate condition, Delegate handler)
    {
        Id = id;
        Information = information;
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