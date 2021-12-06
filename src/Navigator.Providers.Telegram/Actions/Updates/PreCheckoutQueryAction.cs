using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions.Updates
{
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
}