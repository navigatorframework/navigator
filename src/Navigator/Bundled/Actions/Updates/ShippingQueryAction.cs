using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// action triggered by a shipping query being sent.
/// </summary>
[ActionType(nameof(ShippingQueryAction))]
public abstract class ShippingQueryAction : BaseAction
{
    /// <inheritdoc />
    protected ShippingQueryAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

}