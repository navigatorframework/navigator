using Navigator.Context;
using Navigator.Providers.Telegram.Actions;
using Navigator.Providers.Telegram.Actions.Updates;

namespace Navigator.Samples.Echo.Actions
{
    public class EditedEchoAction : EditedMessageAction
    {
        public EditedEchoAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
        }

        public override bool CanHandleCurrentContext()
        {
            return true;
        }
    }
}