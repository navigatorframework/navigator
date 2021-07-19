using Navigator.Actions;
using Navigator.Actions.Model;
using Navigator.Context;
using Navigator.Context.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions
{
    /// <summary>
    /// A message based action.
    /// </summary>
    public abstract class MessageAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(MessageAction));

        /// <inheritdoc />
        public override ushort Priority { get; protected set; } = Navigator.Actions.Priority.Default;

        /// <inheritdoc />
        public override IAction Init(INavigatorContext navigatorContext)
        {
            var update = navigatorContext.GetOriginalUpdateOrDefault<Update>();

            if (update is not null)
            {
                Message = update.Message;
                IsReply = update.Message.ReplyToMessage is not null;
                IsForwarded = update.Message.ForwardDate is not null;
            }

            return this;    
        }
        
        public Message Message { get; protected set; } = null!;

        public bool IsReply { get; protected set; }
        
        public bool IsForwarded { get; protected set; }
    }
}