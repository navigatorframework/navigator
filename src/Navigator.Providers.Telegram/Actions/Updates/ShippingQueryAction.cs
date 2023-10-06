using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions.Updates;

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