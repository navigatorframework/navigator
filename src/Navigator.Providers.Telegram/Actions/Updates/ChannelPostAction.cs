using Navigator.Actions;
using Navigator.Context;

namespace Navigator.Providers.Telegram.Actions.Updates
{
    /// <summary>
    /// TODO
    /// </summary>
    public abstract class ChannelPostAction : BaseAction
    {
        /// <inheritdoc />
        public override string Type { get; protected set; } = typeof(ChannelPostAction).FullName!;

        /// <inheritdoc />
        protected ChannelPostAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            //TODO
        }

        

    }
}