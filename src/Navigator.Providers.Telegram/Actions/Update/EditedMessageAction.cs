using Navigator.Actions;
using Navigator.Context;
using Navigator.Context.Extensions;

namespace Navigator.Providers.Telegram.Actions.Update
{
    public abstract class EditedMessageAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(EditedMessageAction).FullName!;

        /// <inheritdoc />
        public override IAction Init(INavigatorContext navigatorContext)
        {
            var update = navigatorContext.GetOriginalUpdateOrDefault<global::Telegram.Bot.Types.Update>();

            if (update is not null)
            {
                OriginalMessage = update.EditedMessage;
            }
            
            return this;
        }
        
        public global::Telegram.Bot.Types.Message OriginalMessage { get; protected set; } = null!;
        public global::Telegram.Bot.Types.Message EditedMessage { get; protected set; } = null!;

    }
}