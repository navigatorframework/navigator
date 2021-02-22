using Navigator.Actions;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions
{
    public abstract class CommandAction : BaseAction
    {
        public override string Type { get; protected set; } = ActionHelper.Type.For<TelegramProvider>(nameof(CommandAction));
        public override ushort Priority { get; protected set; } = ActionHelper.Priority.Default;
    }
}