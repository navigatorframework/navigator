using Navigator.Actions;
using Navigator.Actions.Model;

namespace Navigator.Providers.Telegram.Actions
{
    public abstract class UnknownAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = ActionsHelper.Type.For<TelegramNavigatorProvider>(nameof(UnknownAction));

        /// <inheritdoc />
        public override ushort Priority { get; protected set; } = ActionsHelper.Priority.Default;
    }
}