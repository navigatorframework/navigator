using MediatR;
using Navigator.Context;
using Navigator.Context.Accessor;

namespace Navigator.Actions;

public abstract class ActionHandler<TAction> : IRequestHandler<TAction, Status> where TAction : IAction
{
    public readonly INavigatorContext Context;

    protected ActionHandler(INavigatorContextAccessor navigatorContextAccessor)
    {
        Context = navigatorContextAccessor.NavigatorContext;
    }

    public abstract Task<Status> Handle(TAction action, CancellationToken cancellationToken);

    public static Status Success()
    {
        return new(true);
    }

    public static Status Error()
    {
        return new(false);
    }
}