using Navigator.Actions;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions.Updates
{
    /// <summary>
    /// TODO
    /// </summary>
    public abstract class SupergroupCreatedAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(SupergroupCreatedAction).FullName!;

        /// <inheritdoc />
        protected SupergroupCreatedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            //TODO
        }

    }
}