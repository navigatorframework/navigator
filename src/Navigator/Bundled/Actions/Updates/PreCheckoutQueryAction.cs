using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;

namespace Navigator.Bundled.Actions.Updates;

/// <summary>
/// Action triggered by a pre-checkout query being sent.
/// </summary>
[ActionType(nameof(PreCheckoutQueryAction))]
public abstract class PreCheckoutQueryAction : BaseAction
{
    /// <inheritdoc />
    protected PreCheckoutQueryAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
    }

}