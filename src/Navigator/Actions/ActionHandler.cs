using MediatR;
using Navigator.Context;

namespace Navigator.Actions;

public abstract class ActionHandler<TAction> : IRequestHandler<TAction, Status> where TAction : IAction
{
    public INavigatorContext NavigatorContext;

    protected ActionHandler(INavigatorContextAccessor navigatorContextAccessor)
    {
        NavigatorContext = navigatorContextAccessor.NavigatorContext;
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