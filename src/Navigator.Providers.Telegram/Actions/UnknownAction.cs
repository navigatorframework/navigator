using Navigator.Actions;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions
{
    public abstract class UnknownAction : BaseAction
    { 
        /// <inheritdoc />
        protected UnknownAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
        }
    }
}