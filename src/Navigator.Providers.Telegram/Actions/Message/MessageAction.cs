using Navigator.Actions;
using Navigator.Context;
using Navigator.Context.Extensions;

namespace Navigator.Providers.Telegram.Actions.Message
{
    /// <summary>
    /// A message based action.
    /// </summary>
    public abstract class MessageAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(MessageAction).FullName!;

        /// <inheritdoc />
        public override IAction Init(INavigatorContext navigatorContext)
        {
            var update = navigatorContext.GetOriginalUpdateOrDefault<global::Telegram.Bot.Types.Update>();

            if (update is not null)
            {
                Message = update.Message;
                IsReply = update.Message.ReplyToMessage is not null;
                IsForwarded = update.Message.ForwardDate is not null;
            }

            return this;    
        }
        
        /// <summary>
        /// The original Message.
        /// </summary>
        public global::Telegram.Bot.Types.Message Message { get; protected set; } = null!;

        /// <summary>
        /// Determines if this message is a reply to another message.
        /// </summary>
        public bool IsReply { get; protected set; }
        
        /// <summary>
        /// Determines if this message is a forwarded message.
        /// </summary>
        public bool IsForwarded { get; protected set; }
    }
}