using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Bundled.Actions;

/// <summary>
/// Represents an unknown action.
/// If you are seeing this, something probably went wrong.
/// </summary>
[ActionType(nameof(UnknownAction))]
public abstract class UnknownAction : BaseAction
{ 
    /// <inheritdoc />
    protected UnknownAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }
}