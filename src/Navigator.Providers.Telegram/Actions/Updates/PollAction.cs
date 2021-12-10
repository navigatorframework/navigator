using Navigator.Actions;
using Navigator.Actions.Attributes;
using Navigator.Context;
using Navigator.Context.Extensions;
using Navigator.Context.Extensions.Bundled;
using Navigator.Context.Extensions.Bundled.OriginalEvent;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Updates
{
    /// <summary>
    /// TODO
    /// </summary>
    [ActionType(nameof(PollAction))]
    public abstract class PollAction : BaseAction
    {
        /// <summary>
        /// The original Poll.
        /// </summary>
        public Poll Poll { get; protected set; }

        /// <inheritdoc />
        protected PollAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            var update = NavigatorContextAccessor.NavigatorContext.GetOriginalEvent<Update>();

            Poll = update.Poll;
        }
    }
}