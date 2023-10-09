using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages
{
    /// <summary>
    /// Action triggered by a location being sent.
    /// </summary>
    [ActionType(nameof(LocationAction))]
    public abstract class LocationAction : MessageAction
    {
        /// <summary>
        /// Location information.
        /// </summary>
        public readonly Location Location;

        /// <inheritdoc />
        protected LocationAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            Location = Message.Location!; // Assuming Location is a property of the Message class.
        }
    }
}