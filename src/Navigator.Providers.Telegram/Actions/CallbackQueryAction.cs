using Navigator.Actions;
using Navigator.Actions.Model;

namespace Navigator.Providers.Telegram.Actions
{
    public abstract class CallbackQueryAction : BaseAction
    {
        public override string Type { get; protected set; } = ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(CallbackQueryAction));
        public override ushort Priority { get; protected set; } = ActionsHelper.Priority.Default;
    }
}