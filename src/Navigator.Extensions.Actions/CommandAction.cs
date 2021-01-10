using System;
using Navigator.Abstractions;
using Action = Navigator.Actions.Action;

namespace Navigator.Extensions.Actions
{
    public abstract class CommandAction : Action
    {
        public override string Type => ActionType.Command;
        public DateTime MessageTimestamp { get; protected set; }
        public int MessageId { get; protected set; }
        public int? ReplyToMessageId { get; protected set; }
        public string Command { get; set; } = string.Empty;
        
        public override IAction Init(INavigatorContext ctx)
        {
            MessageTimestamp = ctx.Update.Message.Date;
            MessageId = ctx.Update.Message.MessageId;
            ReplyToMessageId = ctx.Update.Message.ReplyToMessage?.MessageId;

            Command = ctx.Update.Message.ExtractCommand(ctx.BotProfile.Username) ?? string.Empty;

            return this;
        }
    }
}