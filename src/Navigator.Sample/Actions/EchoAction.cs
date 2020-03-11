using Navigator.Abstraction;
using Action = Navigator.Actions.Action;

namespace Navigator.Sample.Actions
{
    public class EchoAction : Action
    {
        public override string Type => ActionType.Message;
        public string EchoMessage { get; protected set; }
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