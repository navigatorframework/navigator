using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types.Payments;

namespace Navigator.Bundled.Actions.Messages;

/// <summary>
/// Action triggered by an invoice being sent.
/// </summary>
[ActionType(nameof(InvoiceAction))]
public abstract class InvoiceAction : MessageAction
{
    /// <summary>
    /// Invoice information.
    /// </summary>
    public readonly Invoice Invoice;

    /// <inheritdoc />
    protected InvoiceAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
    {
        Invoice = Message.Invoice!;
    }
}