using System;
using Navigator.Abstractions;
using Action = Navigator.Actions.Action;

namespace Navigator.Extensions.Actions
{
    public abstract class MessageAction : Action
    {
        public override string Type => ActionType.Message;
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
    }
}