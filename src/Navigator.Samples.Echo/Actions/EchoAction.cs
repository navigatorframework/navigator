using Navigator.Context;
using Navigator.Providers.Telegram.Actions.Messages;

namespace Navigator.Samples.Echo.Actions
{
    public class EchoAction : MessageAction
    {
        public readonly string MessageToEcho;

        public EchoAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            MessageToEcho = !string.IsNullOrWhiteSpace(Message.Text) ? Message.Text : string.Empty;
        }

        public override bool CanHandleCurrentContext()
        {
            return !string.IsNullOrWhiteSpace(MessageToEcho);
        }
    }
}