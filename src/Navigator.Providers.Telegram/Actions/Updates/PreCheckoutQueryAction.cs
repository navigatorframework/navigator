using Navigator.Actions;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions.Updates
{
    /// <summary>
    /// TODO
    /// </summary>
    public abstract class PreCheckoutQueryAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(PreCheckoutQueryAction).FullName!;

        /// <inheritdoc />
        protected PreCheckoutQueryAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            //TODO
        }

    }
}