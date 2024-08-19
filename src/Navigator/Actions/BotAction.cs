namespace Navigator.Actions;

public sealed record BotAction
{
    public readonly Guid Id; 
    public readonly BotActionInformation Information;
    private readonly Delegate Condition;
    private readonly Delegate Handler;

    public BotAction(Guid id, BotActionInformation information, Delegate condition, Delegate handler)
    {
        Id = id;
        Information = information;
        Condition = condition;
        Handler = handler;
    }
    
    public async Task<bool> ExecuteCondition(params object[] args)
    {
        var result = Condition.DynamicInvoke(args);

        return result switch
        {
            Task<bool> task => await task,
            bool b => b,
            //TODO: specify exception
            _ => throw new NavigatorException()
        };

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