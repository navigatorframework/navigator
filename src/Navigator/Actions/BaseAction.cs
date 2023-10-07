using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Accessor;

namespace Navigator.Actions;

/// <summary>
/// Base action to use for any action.
/// </summary>
[ActionPriority(Actions.Priority.Default)]
public abstract class BaseAction : IAction
{
    private readonly INavigatorContextAccessor _navigatorContextAccessor;
    
    /// <summary>
    /// Used to access <see cref="INavigatorContext"/> inside the action.
    /// </summary>
    protected INavigatorContext Context => _navigatorContextAccessor.NavigatorContext;

    public virtual ushort Priority { get; protected set; } = Actions.Priority.Default;
        
    /// <summary>
    /// Timestamp of the request on creation.
    /// </summary>
    public DateTime Timestamp { get; }

    /// <summary>
    /// Default constructor for <see cref="BaseAction"/>
    /// </summary>
    public BaseAction(INavigatorContextAccessor navigatorContextAccessor)
    {
        _navigatorContextAccessor = navigatorContextAccessor;
        Timestamp = DateTime.UtcNow;
    }

    /// <inheritdoc />
    public abstract bool CanHandleCurrentContext();
}