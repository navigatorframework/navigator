using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Bundled.Actions;

[ActionType(nameof(UnknownAction))]
public abstract class UnknownAction : BaseAction
{ 
    /// <inheritdoc />
    protected UnknownAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }
}