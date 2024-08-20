namespace Navigator.Actions;

public sealed record BotAction
{
    private readonly Delegate Condition;
    private readonly Delegate Handler;
    public readonly Guid Id;
    public readonly BotActionInformation Information;

    public BotAction(Guid id, BotActionInformation information, Delegate condition, Delegate handler)
    {
        Id = id;
        Information = information;
        Condition = condition;
        Handler = handler;
    }

    public async Task<bool> ExecuteCondition(object?[] args)
    {
        // var result = Condition.DynamicInvoke(args.First());
        var result = Condition.Method.Invoke(Condition.Target, args);

        return result switch
        {
            Task<bool> task => await task,
            bool b => b,
            //TODO: specify exception
            _ => throw new NavigatorException()
        };
    }

    public async Task ExecuteHandler(object?[] args)
    {
        var result = Handler.Method.Invoke(Handler.Target, args);

        if (result is Task task) await task;
    }
}