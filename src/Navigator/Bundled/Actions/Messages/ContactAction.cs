using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages
{
    /// <summary>
    /// Action triggered by a contact being sent.
    /// </summary>
    [ActionType(nameof(ContactAction))]
    public abstract class ContactAction : MessageAction
    {
        /// <summary>
        /// Contact information.
        /// </summary>
        public readonly Contact Contact;

        /// <inheritdoc />
        protected ContactAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            Contact = Message.Contact!;
        }
    }
}
