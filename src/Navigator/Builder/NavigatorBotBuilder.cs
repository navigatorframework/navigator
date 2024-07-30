using Navigator.Configuration;
using Navigator.Context;
using Telegram.Bot.Types;

namespace Navigator.Builder;

public class BotStrategyBuilder : IBotStrategyBuilder
{
    protected readonly BotAction[] Actions = [];
    
    public IBotActionBuilder OnUpdate(Func<Update, Task<bool>> condition, Func<INavigatorContext, Task> handler)
    {
        Actions.Add(new BotAction(condition, handler));
        
        return 
    }

    public IBotActionBuilder OnUpdate(Func<Update, Task<bool>> condition, Action<INavigatorContext> handler)
    {
        throw new NotImplementedException();
    }

    public IBotActionBuilder OnUpdate(Func<Update, bool> condition, Func<INavigatorContext, Task> handler)
    {
        throw new NotImplementedException();
    }

    public IBotActionBuilder OnUpdate(Func<Update, bool> condition, Action<INavigatorContext> handler)
    {
        throw new NotImplementedException();
    }
}