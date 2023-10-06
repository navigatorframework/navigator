using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Actions.Telegram;

[ActionType(nameof(UnknownAction))]
public abstract class UnknownAction : BaseAction
{ 
    /// <inheritdoc />
    protected UnknownAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }
}