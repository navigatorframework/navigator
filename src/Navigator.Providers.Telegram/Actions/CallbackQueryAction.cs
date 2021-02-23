using Navigator.Actions;

namespace Navigator.Providers.Telegram.Actions
{
    public abstract class CallbackQueryAction : BaseAction
    {
        public override string Type { get; protected set; } = ActionHelper.Type.For<TelegramProvider>(nameof(CallbackQueryAction));
        public override ushort Priority { get; protected set; } = ActionHelper.Priority.Default;
    }
}