using Navigator.Context;
using Navigator.Providers.Telegram.Actions;

namespace Navigator.Samples.Echo.Actions
{
    public class EditedEchoAction : EditedMessageAction
    {
        public override bool CanHandle(INavigatorContext ctx)
        {
            return true;
        }
    }
}