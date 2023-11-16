using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages
{
    /// <summary>
    /// Action triggered by an animation being sent.
    /// </summary>
    [ActionType(nameof(AnimationAction))]
    public abstract class AnimationAction : MessageAction
    {
        /// <summary>
        /// Information about the animation.
        /// </summary>
        public readonly Animation Animation;

        /// <inheritdoc />
        protected AnimationAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            Animation = Message.Animation!;
        }
    }
}