using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace Navigator.Samples.Echo.Actions
{
    public class EchoAction : MessageAction
    {
        public string MessageToEcho { get; set; } = string.Empty;
        
        public override IAction Init(INavigatorContext ctx)
        {
            if (string.IsNullOrWhiteSpace(ctx.Update.Message.Text))
            {
                MessageToEcho = ctx.Update.Message.Text;
            }
            return this;
        }

        public override bool CanHandle(INavigatorContext ctx)
        {
            return !string.IsNullOrWhiteSpace(MessageToEcho);
        }
    }
}