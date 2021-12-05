using Navigator.Actions;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions.Updates
{
    /// <summary>
    /// TODO
    /// </summary>
    public abstract class ChannelCreatedAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(ChannelCreatedAction).FullName!;

        /// <inheritdoc />
        protected ChannelCreatedAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            //TODO
        }

        

    }
}