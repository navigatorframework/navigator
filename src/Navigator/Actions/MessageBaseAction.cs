using System;
using Navigator.Context;

namespace Navigator.Actions
{
    public abstract class MessageAction : BaseAction
    {
        
        public DateTime MessageTimestamp { get; protected set; }
        public int MessageId { get; protected set; }
        public int? ReplyToMessageId { get; protected set; }

        public override IAction Init(INavigatorContext ctx)
        {
            MessageTimestamp = ctx.Update.Message.Date;
            MessageId = ctx.Update.Message.MessageId;
            ReplyToMessageId = ctx.Update.Message.ReplyToMessage?.MessageId;

            return this;
        }

        protected MessageAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
        }
    }
}