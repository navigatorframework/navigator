using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Actions.Telegram.Updates;

/// <summary>
/// TODO
/// </summary>
[ActionType(nameof(PreCheckoutQueryAction))]
public abstract class PreCheckoutQueryAction : BaseAction
{
    /// <inheritdoc />
    protected PreCheckoutQueryAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        //TODO
    }

}