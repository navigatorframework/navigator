using MediatR;
using Navigator.Context;
using Navigator.Context.Accessor;

namespace Navigator.Actions;

/// <summary>
/// Implement to handle an action.
/// </summary>
/// <typeparam name="TAction"></typeparam>
public abstract class ActionHandler<TAction> : IRequestHandler<TAction, Status> where TAction : IAction
{
    /// <summary>
    /// Context for the action.
    /// </summary>
    public readonly INavigatorContext Context;

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="navigatorContextAccessor"></param>
    protected ActionHandler(INavigatorContextAccessor navigatorContextAccessor)
    {
        Context = navigatorContextAccessor.NavigatorContext;
    }

    /// <summary>
    /// Handles the action.
    /// </summary>
    /// <param name="action"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="Status"/></returns>
    public abstract Task<Status> Handle(TAction action, CancellationToken cancellationToken);

    /// <summary>
    /// Called when the action was handled successfully.
    /// </summary>
    /// <returns><see cref="Status"/></returns>
    protected static Status Success()
    {
        return new Status(true);
    }

    /// <summary>
    /// Called when the action was not handled successfully.
    /// </summary>
    /// <returns><see cref="Status"/></returns>
    protected static Status Error()
    {
        return new Status(false);
    }
}