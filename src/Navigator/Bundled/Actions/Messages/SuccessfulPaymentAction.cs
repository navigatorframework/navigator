using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types.Payments;

namespace Navigator.Bundled.Actions.Messages
{
    /// <summary>
    /// Action triggered by a successful payment.
    /// </summary>
    [ActionType(nameof(SuccessfulPaymentAction))]
    public abstract class SuccessfulPaymentAction : MessageAction
    {
        /// <summary>
        /// Successful payment information.
        /// </summary>
        public readonly SuccessfulPayment SuccessfulPayment;

        /// <inheritdoc />
        protected SuccessfulPaymentAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            SuccessfulPayment = Message.SuccessfulPayment!;
        }
    }
}
