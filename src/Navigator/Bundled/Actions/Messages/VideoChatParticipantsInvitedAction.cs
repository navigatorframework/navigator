using Navigator.Actions.Attributes;
using Navigator.Context.Accessor;
using Telegram.Bot.Types;

namespace Navigator.Bundled.Actions.Messages
{
    /// <summary>
    /// Action triggered by participants being invited to a video chat.
    /// </summary>
    [ActionType(nameof(VideoChatParticipantsInvitedAction))]
    public abstract class VideoChatParticipantsInvitedAction : MessageAction
    {
        /// <summary>
        /// Information about the participants invited to the video chat.
        /// </summary>
        public readonly VideoChatParticipantsInvited VideoChatParticipantsInvited;

        /// <inheritdoc />
        protected VideoChatParticipantsInvitedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            VideoChatParticipantsInvited = Message.VideoChatParticipantsInvited!;
        }
    }
}