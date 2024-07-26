using Navigator.Context;
using Navigator.Context.Accessor;

namespace Navigator.Actions;

public abstract record Action
{
    public readonly INavigatorContext Context;

    private Action(INavigatorContextAccessor navigatorContextAccessor)
    {
        Context = navigatorContextAccessor.NavigatorContext;
    }
    
    public abstract Task<bool> CanHandleCurrentContext();

    public abstract Task Handle();
}