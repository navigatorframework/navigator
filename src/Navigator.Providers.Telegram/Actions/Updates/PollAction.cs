using Navigator.Actions;
using Navigator.Context;
using Navigator.Context.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions.Updates
{
    /// <summary>
    /// TODO
    /// </summary>
    public abstract class PollAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(PollAction).FullName!;

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