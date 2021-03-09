using Navigator.Actions.Model;
using Navigator.Context;
using Navigator.Providers.Telegram.Actions;

namespace Navigator.Samples.Echo.Actions
{
    public class EchoAction : MessageAction
    {
        public EchoAction()
        {
            MessageToEcho = string.Empty;
        }

        public string MessageToEcho { get; set; }
        
        public override IAction Init(INavigatorContext ctx)
        {
            base.Init(ctx);
            
            if (!string.IsNullOrWhiteSpace(Message.Text))
            {
                MessageToEcho = Message.Text;
            }
            
            return this;
        }

        public override bool CanHandle(INavigatorContext ctx)
        {
            return !string.IsNullOrWhiteSpace(MessageToEcho);
        }
    }
}