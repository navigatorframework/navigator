using Navigator.Abstractions;
using Navigator.Extensions.Actions;

namespace Navigator.Samples.Echo.Actions
{
    public class EchoAction : Action
    {
        public override string Type => ActionType.Message;
        public string EchoMessage { get; private set; } = string.Empty;
        public override IAction Init(INavigatorContext ctx)
        {
            EchoMessage = ctx.Update.Message.Text;
            return this;
        }

        public override bool CanHandle(INavigatorContext ctx)
        {
            return true;
        }
    }
}