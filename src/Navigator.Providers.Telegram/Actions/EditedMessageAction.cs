using Navigator.Actions;
using Navigator.Actions.Model;
using Navigator.Context;
using Navigator.Context.Extensions;
using Telegram.Bot.Types;

namespace Navigator.Providers.Telegram.Actions
{
    public abstract class EditedMessageAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(EditedMessageAction));

        /// <inheritdoc />
        public override ushort Priority { get; protected set; } =Navigator.Actions.Priority.Default;

        /// <inheritdoc />
        public override IAction Init(INavigatorContext navigatorContext)
        {
            var update = navigatorContext.GetOriginalUpdateOrDefault<Update>();

            if (update is not null)
            {
                OriginalMessage = update.EditedMessage;
            }
            
            return this;
        }
        
        public Message OriginalMessage { get; protected set; } = null!;
        public Message EditedMessage { get; protected set; } = null!;

    }
}