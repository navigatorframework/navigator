using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Accessor;

namespace Navigator.Actions;

[ActionPriority(Actions.Priority.Default)]
public abstract record Action
{
    private readonly INavigatorContextAccessor _navigatorContextAccessor;
    
    protected INavigatorContext Context => _navigatorContextAccessor.NavigatorContext;

    public readonly DateTime Timestamp;

    protected Action(INavigatorContextAccessor navigatorContextAccessor)
    {
        _navigatorContextAccessor = navigatorContextAccessor;
        Timestamp = DateTime.UtcNow;
    }

    public abstract bool CanHandleCurrentContext(CancellationToken cancellationToken = default);

    public abstract Task<Status> Handle(CancellationToken cancellationToken = default);

    protected static Status Success()
    {
        return new Status(true);
    }

    protected static Status Error()
    {
        return new Status(false);
    }
}