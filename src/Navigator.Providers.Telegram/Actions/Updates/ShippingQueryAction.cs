using Navigator.Actions;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions.Updates
{
    /// <summary>
    /// TODO
    /// </summary>
    public abstract class ShippingQueryAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(ShippingQueryAction).FullName!;

        /// <inheritdoc />
        protected ShippingQueryAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            //TODO
        }

    }
}