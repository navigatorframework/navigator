using Navigator.Actions;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions;
using Navigator.Providers.Telegram.Actions.Messages;

namespace Navigator.Samples.Echo.Actions
{
    public class EchoAction : MessageAction
    {
        public override ushort Priority { get; protected set; } = Navigator.Actions.Priority.High;

        public string MessageToEcho { get; protected set; } = string.Empty;

        public EchoAction(INavigatorContextAccessor navigatorContextAccessor) : base(navigatorContextAccessor)
        {
            if (!string.IsNullOrWhiteSpace(Message.Text))
            {
                MessageToEcho = Message.Text;
            }
        }

        public override bool CanHandleCurrentContext()
        {
            return !string.IsNullOrWhiteSpace(MessageToEcho);
        }
    }
}