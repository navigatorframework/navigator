using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Actions.Telegram.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(ShippingQueryAction))]
public abstract class ShippingQueryAction : BaseAction
{
    /// <inheritdoc />
    protected ShippingQueryAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        //TODO
    }

}