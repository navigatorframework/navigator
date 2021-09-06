using Navigator.Actions;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions
{
    public abstract class UnknownAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(UnknownAction).FullName!;

        /// <inheritdoc />
        protected UnknownAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
        }
    }
}