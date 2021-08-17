using Navigator.Actions;

namespace Navigator.Providers.Telegram.Actions
{
    public abstract class UnknownAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(UnknownAction).FullName!;
    }
}